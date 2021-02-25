using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour, ICollisiable
{
    [SerializeField] protected float fallingSpeed;
    protected Rigidbody rigidBody;

    private bool isInPool;
    public bool IsInPool => isInPool;

    private bool takeForce = true;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
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

    protected virtual void Disactivate()
    {
        isInPool = true;
    }
}
