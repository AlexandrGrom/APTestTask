using System;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour, ICollisiable, IPoolable
{
    protected float fallingSpeed = 1;
    protected Rigidbody rigidBody;
    private bool takeForce = true;
    private ItemSpawner itemSpawner;
    protected int isSpeeded;

    private RigidbodyConstraints constraints;

    protected virtual void Awake()
    {
        DataManager.UpdateData += UpdateSpeed;
        rigidBody = GetComponent<Rigidbody>();
        constraints = rigidBody.constraints;
    }

    private void OnDestroy()
    {
        DataManager.UpdateData -= UpdateSpeed;
    }

    protected virtual void UpdateSpeed()
    {
        fallingSpeed = DataManager.EvaluateSpeedByTime() + isSpeeded;
    }

    public virtual void Initialize(ItemSpawner itemSpawner)
    {
        float value = (float)DataManager.EvaluateSpeedByTime() / 7f ;
        value = itemSpawner.EvaluateSpeededСhance.Evaluate(value);
        isSpeeded = value > Random.Range(0f, 1f) ? 1 : 0;

        rigidBody.constraints = constraints;

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
