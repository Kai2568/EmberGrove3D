using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private TMP_Text itemNameText;

    public void Setup(InventorySlot slot)
    {
        if (slot == null || slot.item == null)
        {
            return;
        }

        if (itemIcon != null)
        {
            itemIcon.sprite = slot.item.ItemIcon;

            itemIcon.enabled = slot.item.ItemIcon != null;
        }

        if (quantityText != null)
        {
            quantityText.text = "x" + slot.quantity;
        }

        if (itemNameText != null)
        {
            itemNameText.text = slot.item.ItemName;
        }
    }
}
