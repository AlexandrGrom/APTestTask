using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour, ICollisiable, IPoolable
{
    [SerializeField] protected float fallingSpeed;
    protected Rigidbody rigidBody;

    private bool takeForce = true;
    private ItemSpawner itemSpawner;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Initialize(ItemSpawner itemSpawner)
    {
        this.itemSpawner = itemSpawner;
        transform.SetParent(itemSpawner.transform);
        transform.localPosition = Vector3.zero;
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
