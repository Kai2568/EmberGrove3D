using UnityEngine;

[CreateAssetMenu(
    fileName = "New Item",
    menuName = "Embergrove/Inventory/Item"
)]
public class ItemData : ScriptableObject
{
    [Header("Item Information")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;

    [Header("Stack Settings")]
    [SerializeField] private int maximumStackSize = 99;

    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    public Sprite ItemIcon
    {
        get
        {
            return itemIcon;
        }
    }

    public int MaximumStackSize
    {
        get
        {
            return maximumStackSize;
        }
    }
}