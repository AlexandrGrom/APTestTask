using UnityEngine;

[CreateAssetMenu(fileName = "ItemsHolder", menuName = "ItemsHolder")]
public class ItemsHolder : ScriptableObject
{
    [SerializeField] private Item[] items;
    public Item[] Items => items;
}
