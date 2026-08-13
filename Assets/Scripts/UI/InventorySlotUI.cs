using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private Button slotButton;

    private InventorySlot assignedSlot;
    private ItemDetailsUI itemDetailsUI;

    public void Setup(
        InventorySlot slot,
        ItemDetailsUI detailsUI
    )
    {
        assignedSlot = slot;
        itemDetailsUI = detailsUI;

        if (slot == null || slot.item == null)
        {
            return;
        }

        if (itemIcon != null)
        {
            itemIcon.sprite = slot.item.ItemIcon;
            itemIcon.enabled =
                slot.item.ItemIcon != null;
        }

        if (quantityText != null)
        {
            quantityText.text =
                "x" + slot.quantity;
        }

        if (itemNameText != null)
        {
            itemNameText.text =
                slot.item.ItemName;
        }

        if (slotButton != null)
        {
            slotButton.onClick.RemoveAllListeners();
            slotButton.onClick.AddListener(OnSlotClicked);
        }
    }

    private void OnSlotClicked()
    {
        if (
            assignedSlot == null ||
            itemDetailsUI == null
        )
        {
            return;
        }

        itemDetailsUI.ShowItem(assignedSlot);
    }
}