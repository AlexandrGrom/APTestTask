using UnityEngine;
using DG.Tweening;

public class Egg : Item, IReciveble
{
    public void Recive(Transform parent)
    {
        transform.SetParent(parent);
        float duration = Vector3.Distance(parent.position, transform.position) / 2;
        transform.DOScale(Vector3.zero, duration).SetEase(Ease.OutSine);
        transform.DOLocalMove(Vector3.zero, duration).SetEase(Ease.OutSine);
        DOVirtual.DelayedCall(duration, () => Destroy(gameObject)); // go to pool
    }
}
