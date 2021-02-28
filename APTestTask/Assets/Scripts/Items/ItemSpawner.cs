using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float step;
    [SerializeField] private Vector2Int bombSpawnTime;
    [SerializeField] private Vector2 minMaxOffset;
    [SerializeField] private AnimationCurve evaluateSpeededСhance;
    public AnimationCurve EvaluateSpeededСhance => evaluateSpeededСhance;

    private Item[] itemPrefabs;
    private List<Item> poolingItems = new List<Item>();
    private float offset;
    public float Offset => offset;

    public static int ItemsInSceneCounter { get; private set; }
    private int bombCount;

    private void Awake()
    {
        ItemsInSceneCounter = 0;
        offset = step / 2;
        itemPrefabs = Resources.Load<ItemsHolder>("ItemsHolder").Items;
        StartCoroutine(Spawn(waitTime));
    }

    public void PootInPool(Item item)
    {
        if (item is Bomb)
        {
            bombCount--;
        }
        if (!poolingItems.Contains(item))
        {
            ItemsInSceneCounter--;
            poolingItems.Add(item);
        }
    }

    private Item GetItem(Type type)
    {
        ItemsInSceneCounter++;
        foreach (var item in poolingItems)
        {
            if (item.GetType() == type)
            {
                poolingItems.Remove(item);
                return item;
            }
        }

        foreach (var item in itemPrefabs)
        {
            if (item.GetType() == type)
            {
                Item newItem = Instantiate(item);
                return newItem;
            }
        }
        Debug.LogError("can't find this type");
        return null;
    }


    private IEnumerator Spawn(float waitTime)
    {
        yield return new WaitUntil(() => GameStateManager.CurrentState == GameState.Game);
        while (DataManager.LeftTime >= 0 && GameStateManager.CurrentState != GameState.Lose && GameStateManager.CurrentState != GameState.Win)
        {
            Item item = GetItem(EvaluateTypeByTime().GetType());
            int range = Random.Range(1, 3) * (Random.Range(0, 2) == 0 ? 1 : -1);
            offset += step * range;

            if (offset < minMaxOffset.x)
            {
                offset += step * 3;
            }
            if (offset > minMaxOffset.y)
            {
                offset -= step * 3; 
            }
            item.Initialize(this);

            yield return new WaitForSeconds(waitTime / DataManager.EvaluateSpeedByTime()); 
        }
    }

    private Item EvaluateTypeByTime()
    {
        int idx = Random.Range(0, itemPrefabs.Length);
        var type = itemPrefabs[idx];
        if (DataManager.LeftTime < bombSpawnTime.x || DataManager.LeftTime > bombSpawnTime.y)
        {
            type = itemPrefabs[0];
        }

        if (type is Bomb)
        {
            if (bombCount >= 2)
            {
                type = itemPrefabs[0];
            }
            else
            {
                bombCount++;
            }
        }

        return type;
    }

}
