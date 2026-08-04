using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DragonInventory dragonInventory;
    [SerializeField] private ItemData berryItem;
    [SerializeField] private TMP_Text berryCountText;

    private void Start()
    {
        if (dragonInventory != null)
        {
            dragonInventory.InventoryChanged += UpdateBerryText;
        }

        UpdateBerryText();
    }

    private void OnDestroy()
    {
        if (dragonInventory != null)
        {
            dragonInventory.InventoryChanged -= UpdateBerryText;
        }
    }

    private void UpdateBerryText()
    {
        if (
            dragonInventory == null
            || berryItem == null
            || berryCountText == null
        )
        {
            return;
        }

        int berryCount =
            dragonInventory.GetItemCount(berryItem);

        berryCountText.text =
            "Berries: " + berryCount;
    }
}