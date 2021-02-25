using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private float waitTime;


    private void Awake()
    {
        StartCoroutine(Spawn(waitTime));
    }

    private IEnumerator Spawn(float waitTime)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(waitTime);
        while (true)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            yield return waitForSeconds;
        }
    }
}
