using System;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour, ICollisiable, IPoolable
{
    private float fallingSpeed;
    private Rigidbody rigidBody;
    private bool takeForce = true;
    private ItemSpawner itemSpawner;

    protected virtual void Awake()
    {
        DataManager.UpdateData += UpdateSpeed;
        rigidBody = GetComponent<Rigidbody>();
        Debug.Log(rigidBody == null);
    }

    private void OnDestroy()
    {
        DataManager.UpdateData -= UpdateSpeed;
    }

    private void UpdateSpeed()
    {
        int speededObject = Random.Range(0, DataManager.EvaluateSpeedByTime()) > 2 ? 1 :0 ;
        fallingSpeed = DataManager.EvaluateSpeedByTime() + speededObject;
    }

    public void Initialize(ItemSpawner itemSpawner)
    {
        this.itemSpawner = itemSpawner;
        transform.SetParent(itemSpawner.transform);
        transform.localPosition = new Vector3(itemSpawner.Offset,0,0);
        transform.localScale = Vector3.one;
        takeForce = true;
        rigidBody.useGravity = false;
        rigidBody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }


    private void FixedUpdate()
    {
        if (takeForce)
        {
            rigidBody.velocity = Vector3.down * fallingSpeed;
        }
    }

    public virtual void Collide()
    {
        takeForce = false;
        rigidBody.useGravity = true;
    }

    public void GoToPool()
    {
        itemSpawner.PootInPool(this);
    }
}
