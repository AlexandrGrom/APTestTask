using UnityEngine;

public abstract class ScreenElement : MonoBehaviour
{
    [SerializeField] protected GameState state;
    public GameState State => state;

    protected virtual void OnEnable()
    {
        Reset();
        Animation();
    }

    protected abstract void Animation();


    protected abstract void Reset();

}
