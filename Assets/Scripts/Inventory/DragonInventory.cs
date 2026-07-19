using System.Collections.Generic;
using UnityEngine;

public class DragonInventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public void AddItem(ItemData item, int amount)
    {
        if (item == null)
        {
            Debug.LogWarning(
                "The item being added to the inventory is missing.");

            return;
        }

        if (amount <= 0)
        {
            return;
        }

        InventorySlot existingSlot = FindSlot(item);

        if (existingSlot != null)
        {
            existingSlot.quantity += amount;
        }
        else
        {
            InventorySlot newSlot = new InventorySlot(item, amount);

            inventorySlots.Add(newSlot);
        }

        Debug.Log(
            "Added "
            + amount
            + " "
            + item.ItemName
            + ". New Total: "
            + GetItemCount(item));
    }

   public int GetItemCount(ItemData item)
    {
        InventorySlot slot = FindSlot(item);

        if (slot == null)
        {
            return 0;
        }

        return slot.quantity;
    }

    private InventorySlot FindSlot(ItemData item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == item)
            {
                return inventorySlots[i];
            }
        }

        return null;
    }
}
