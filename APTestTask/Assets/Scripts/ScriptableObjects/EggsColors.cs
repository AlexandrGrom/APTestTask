using UnityEngine;

[CreateAssetMenu(fileName = "EggsColors", menuName = "EggsColors")]

public class EggsColors : ScriptableObject
{
    [SerializeField] private Color[] colors;
    public Color[] Colors => colors;
}
