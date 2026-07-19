using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DragonInventory dragonInventory;
    [SerializeField] private ItemData berryItem;
    [SerializeField] private TMP_Text berryCountText;

    private void Update()
    {
        UpdateBerryText();
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