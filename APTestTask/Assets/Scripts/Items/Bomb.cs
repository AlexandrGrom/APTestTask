using UnityEngine;
using DG.Tweening;


public class Bomb : Item, IReciveble
{
    public void Recive(Transform parent)
    {
        GameStateManager.CurrentState = GameState.Lose; //plat
        transform.SetParent(parent);
        float duration = Vector3.Distance(parent.position, transform.position) / 2;
        transform.DOScale(Vector3.zero, duration).SetEase(Ease.OutSine);
        transform.DOLocalMove(Vector3.zero, duration).SetEase(Ease.OutSine);
        DOVirtual.DelayedCall(duration, () => GoToPool());
    }
}
