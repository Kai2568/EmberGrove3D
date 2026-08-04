using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonInventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField]
    private List<InventorySlot> inventorySlots =
        new List<InventorySlot>();

    [Header("Starting Items")]
    [SerializeField] private ItemData startingItem;
    [SerializeField] private int startingAmount;

    private void Start()
    {
        if (startingItem != null && startingAmount > 0)
        {
            AddItem(startingItem, startingAmount);
        }
    }

    // Other scripts can subscribe to this event.
    // It runs whenever an item is added or removed.
    public event Action InventoryChanged;

    public void AddItem(ItemData item, int amount)
    {
        if (item == null)
        {
            Debug.LogWarning(
                "The item being added to the inventory is missing."
            );

            return;
        }

        if (amount <= 0)
        {
            Debug.LogWarning(
                "The amount being added must be greater than zero."
            );

            return;
        }

        InventorySlot existingSlot = FindSlot(item);

        if (existingSlot != null)
        {
            existingSlot.quantity += amount;
        }
        else
        {
            InventorySlot newSlot =
                new InventorySlot(item, amount);

            inventorySlots.Add(newSlot);
        }

        Debug.Log(
            "Added "
            + amount
            + " "
            + item.ItemName
            + ". New total: "
            + GetItemCount(item)
        );

        InventoryChanged?.Invoke();
    }

    public bool RemoveItem(ItemData item, int amount)
    {
        if (item == null)
        {
            Debug.LogWarning(
                "The item being removed is missing."
            );

            return false;
        }

        if (amount <= 0)
        {
            Debug.LogWarning(
                "The amount being removed must be greater than zero."
            );

            return false;
        }

        InventorySlot slot = FindSlot(item);

        if (slot == null)
        {
            Debug.Log(
                "The inventory does not contain "
                + item.ItemName
                + "."
            );

            return false;
        }

        if (slot.quantity < amount)
        {
            Debug.Log(
                "Not enough "
                + item.ItemName
                + ". Required: "
                + amount
                + " | Available: "
                + slot.quantity
            );

            return false;
        }

        slot.quantity -= amount;

        if (slot.quantity <= 0)
        {
            inventorySlots.Remove(slot);
        }

        Debug.Log(
            "Removed "
            + amount
            + " "
            + item.ItemName
            + ". Remaining: "
            + GetItemCount(item)
        );

        InventoryChanged?.Invoke();

        return true;
    }

    public bool HasItem(ItemData item, int requiredAmount = 1)
    {
        if (item == null || requiredAmount <= 0)
        {
            return false;
        }

        return GetItemCount(item) >= requiredAmount;
    }

    public int GetItemCount(ItemData item)
    {
        if (item == null)
        {
            return 0;
        }

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