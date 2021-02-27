using UnityEngine;
using DG.Tweening;

public class Egg : Item, IReciveble
{
    [SerializeField] private MeshRenderer mesh;
    protected override void Awake()
    {
        base.Awake();
        Color[] colors = Resources.Load<EggsColors>("EggsColors").Colors;
        mesh.material.color = colors[Random.Range(0, colors.Length)];
    }
    public void Recive(Transform parent)
    {
        transform.SetParent(parent);
        float duration = Vector3.Distance(parent.position, transform.position) / 2;
        transform.DOScale(Vector3.zero, duration).SetEase(Ease.OutSine);
        transform.DOLocalMove(Vector3.zero, duration).SetEase(Ease.OutSine);
        DOVirtual.DelayedCall(duration, () => GoToPool());
        if (GameStateManager.CurrentState == GameState.Game)
        {
            DataManager.IncrementCounter();
        }
    }
}
