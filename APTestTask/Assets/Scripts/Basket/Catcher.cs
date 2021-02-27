using UnityEngine;

public class Catcher : MonoBehaviour
{
    [SerializeField] private int fine = 5;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPoolable poolable))
        {
            poolable.GoToPool();
            if (poolable is Egg)
            {
                DataManager.DecrementTime(fine);
            }
        }
    }
}
