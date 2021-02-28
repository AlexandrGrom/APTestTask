using UnityEngine;
using DG.Tweening;

public class ItemsReceiver : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private int strength;
    [SerializeField] private int vibro;
    [SerializeField] private ParticleSystem particle;

    private Tween tween;


    private void OnTriggerEnter(Collider other)
    {
        if (GameStateManager.CurrentState != GameState.Game) return;

        if (other.TryGetComponent(out IReciveble reciveble))
        {
            tween.Kill(true);
            reciveble.Recive(transform);
            tween = transform.DOShakeRotation(duration, strength, vibro).OnComplete(()=> transform.rotation = Quaternion.identity);

            if (reciveble is Bomb)
            {
                particle.Play();
            }
        }
    }

}
