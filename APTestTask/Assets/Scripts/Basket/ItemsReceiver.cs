using UnityEngine;

public class ItemsReceiver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IReciveble reciveble))
        {
            reciveble.Recive(transform);
        }
    }

}
