using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private List<Item> itemPrefabs;
    private List<Item> poolingItems = new List<Item>();

    private Item GetItem(Type type)
    {
        foreach (var item in poolingItems)
        {
            Type targetType = item.GetType();
            
            if (targetType == type)
            {
                poolingItems.Remove(item);
                Debug.Log("object was gotten from pool");
                return item;
            }
        }

        foreach (var item in itemPrefabs)
        {
            Type v = item.GetType();

            if (v == type)
            {
                Item newItem = Instantiate(item);
                return newItem;
            }
        }
        Debug.LogError("can't find this type");
        return null;
    }

    public void PootInPool(Item item)
    {
        if (!poolingItems.Contains(item))
        {
            poolingItems.Add(item);
        }
    }


    private void Awake()
    {
        StartCoroutine(Spawn(waitTime));
    }

    private IEnumerator Spawn(float waitTime)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => GameStateManager.CurrentState == GameState.Game);
        while (GameStateManager.CurrentState == GameState.Game)
        {
            Item item = GetItem(typeof(Egg));
            item.Initialize(this);

            yield return waitForSeconds;
        }
    }
}
