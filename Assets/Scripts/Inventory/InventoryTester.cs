using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DragonInventory dragonInventory;
    [SerializeField] private ItemData testItem;

    [Header("Testing")]
    [SerializeField] private int amountToRemove = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TestRemoveItem();
        }
    }

    private void TestRemoveItem()
    {
        if (dragonInventory == null || testItem == null)
        {
            Debug.LogWarning(
                "InventoryTester references are missing."
            );

            return;
        }

        bool removedSuccessfully =
            dragonInventory.RemoveItem(
                testItem,
                amountToRemove
            );

        if (removedSuccessfully)
        {
            Debug.Log(
                "Inventory removal test succeeded."
            );
        }
        else
        {
            Debug.Log(
                "Inventory removal test failed."
            );
        }
    }
}