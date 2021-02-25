using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasketMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private Vector2 MinMaxScreenOffset;

    private Rigidbody rigidBody;
    private Vector3 currentVelocity;
    private Vector3 destination;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        destination = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 screenMouseposition = Input.mousePosition;
            screenMouseposition.x = Mathf.Clamp
                (screenMouseposition.x,
                MinMaxScreenOffset.x * Screen.width,
                MinMaxScreenOffset.y * Screen.width);

            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(screenMouseposition);
            destination = new Vector3(worldMousePosition.x, transform.position.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = (destination - transform.position) * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Item item))
        {
            item.Collide();
        }
    }
}
