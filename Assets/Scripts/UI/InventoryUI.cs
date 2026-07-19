using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DragonInventory dragonInventory;
    [SerializeField] private TMP_Text berryCountText;

    private void Start()
    {
        UpdateBerryText();
    }

    private void Update()
    {
        UpdateBerryText();
    }

    private void UpdateBerryText()
    {
        if (dragonInventory == null || berryCountText == null)
        {
            return;
        }

        berryCountText.text = "Berries: " + dragonInventory.BerryCount;
    }
}
