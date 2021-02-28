using TMPro;
using UnityEngine;
using DG.Tweening;

public class Catcher : MonoBehaviour
{
    [SerializeField] private int fine = 5;
    [SerializeField] private TextMeshPro text;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPoolable poolable))
        {
            poolable.GoToPool();
            if (GameStateManager.CurrentState != GameState.Game) return;

            if (poolable is Egg)
            {
                text.transform.position = other.transform.position;
                text.transform.DOMoveY(text.transform.position.y + 4.5f, 0.3f).SetEase(Ease.OutBack).OnComplete(()=> text.transform.localPosition = Vector3.zero);
                text.text = $"-{fine}";
                DataManager.DecrementTime(fine);
            }
        }
    }
}
