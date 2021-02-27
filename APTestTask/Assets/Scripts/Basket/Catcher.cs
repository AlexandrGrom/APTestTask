using UnityEngine;

public class Catcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPoolable poolable))
        {
            Debug.Log("chaced");
            poolable.GoToPool();
        }
    }
}
