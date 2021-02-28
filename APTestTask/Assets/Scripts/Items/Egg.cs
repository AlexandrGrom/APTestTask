using UnityEngine;
using DG.Tweening;

public class Egg : Item, IReciveble
{
    [SerializeField] private MeshRenderer mesh;
    bool wasRecived = false;

    protected override void Awake()
    {
        base.Awake();
        Color[] colors = Resources.Load<EggsColors>("EggsColors").Colors;
        mesh.material.color = colors[Random.Range(0, colors.Length)];
    }

    public override void Initialize(ItemSpawner itemSpawner)
    {
        base.Initialize(itemSpawner);
        wasRecived = false;
    }

    public void Recive(Transform parent)
    {
        if (wasRecived) return;

        wasRecived = true;
        transform.SetParent(parent);
        float duration = Vector3.Distance(parent.position, transform.position) / 2;
        transform.DOScale(Vector3.zero, duration).SetEase(Ease.OutSine);
        transform.DOLocalMove(Vector3.zero, duration).SetEase(Ease.OutSine);
        DOVirtual.DelayedCall(duration, () => 
        {
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            GoToPool();
        });

        if (GameStateManager.CurrentState == GameState.Game)
        {
            DataManager.IncrementCounter();
        }
    }
}
