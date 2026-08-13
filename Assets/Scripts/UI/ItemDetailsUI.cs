using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject detailsPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private DragonInventory dragonInventory;

    private InventorySlot selectedSlot;

    private void Awake()
    {
        HideDetails();
    }

    public void ShowItem(InventorySlot slot)
    {
        if (slot == null || slot.item == null)
        {
            HideDetails();
            return;
        }

        selectedSlot = slot;

        if (detailsPanel != null)
        {
            detailsPanel.SetActive(true);
        }

        if (itemIcon != null)
        {
            itemIcon.sprite = slot.item.ItemIcon;
            itemIcon.enabled = slot.item.ItemIcon != null;
        }

        if (itemNameText != null)
        {
            itemNameText.text = slot.item.ItemName;
        }

        if (quantityText != null)
        {
            quantityText.text = "Quantity: " + slot.quantity;
        }

        if (descriptionText != null)
        {
            descriptionText.text = slot.item.ItemDescription;
        }
    }

    public void SelectCurrentItem()
    {
        if (
            dragonInventory == null ||
            selectedSlot == null ||
            selectedSlot.item == null
        )
        {
            return;
        }

        dragonInventory.SelectItem(
            selectedSlot.item
        );
    }

    public void HideDetails()
    {
        if (detailsPanel != null)
        {
            detailsPanel.SetActive(false);
        }
    }
}
