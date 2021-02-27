using UnityEngine;

public abstract class ScreenElement : MonoBehaviour
{
    [SerializeField] protected GameState state;
    public GameState State => state;
}
