using UnityEngine;

[CreateAssetMenu(
    fileName = "New Item",
    menuName = "Embergrove/Inventory/Item"
)]
public class ItemData : ScriptableObject
{
    [Header("Item Information")]
    [SerializeField] private string itemName;

    [TextArea(2, 5)]
    [SerializeField] private string itemDescription;

    [SerializeField] private Sprite itemIcon;

    [Header("Stack Settings")]
    [SerializeField] private int maximumStackSize = 99;

    [Header("Farming")]
    [SerializeField] private CropData cropData;

    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    public string ItemDescription
    {
        get
        {
            return itemDescription;
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

    public CropData CropData
    {
        get
        {
            return cropData;
        }
    }
}