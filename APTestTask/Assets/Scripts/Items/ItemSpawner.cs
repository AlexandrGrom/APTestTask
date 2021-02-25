using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private Item[] item;
    private List<Item> poolingItems = new List<Item>();


    private void Awake()
    {
        StartCoroutine(Spawn(waitTime));
    }

    private IEnumerator Spawn(float waitTime)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(waitTime);
        while (true)
        {
            Instantiate(item[Random.Range(0, item.Length)], transform.position, Quaternion.identity);
            yield return waitForSeconds;
        }
    }
}
