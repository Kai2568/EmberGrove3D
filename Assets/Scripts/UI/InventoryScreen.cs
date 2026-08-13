using UnityEngine;

public class InventoryScreen : MonoBehaviour
{
    [Header("Dragon Controls")]
    [SerializeField] private DragonMovement dragonMovement;
    [SerializeField] private DragonInteractor dragonInteractor;
    [SerializeField] private DragonToolController toolController;

    [Header("Inventory")]
    [SerializeField] private DragonInventory dragonInventory;

    [Header("UI")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private ItemDetailsUI itemDetailsUI;

    [Header("Prefabs")]
    [SerializeField] private InventorySlotUI slotPrefab;

    [Header("Controls")]
    [SerializeField] private KeyCode inventoryKey = KeyCode.I;

    private bool inventoryOpen;


    private void Start()
    {
        if (dragonInventory != null)
        {
            dragonInventory.InventoryChanged += RefreshInventory;
        }

        CloseInventory();
    }


    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }
    }


    private void OnDestroy()
    {
        if (dragonInventory != null)
        {
            dragonInventory.InventoryChanged -= RefreshInventory;
        }
    }


    private void ToggleInventory()
    {
        if (inventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }


    private void OpenInventory()
    {
        inventoryOpen = true;

        if (itemDetailsUI != null)
        {
            itemDetailsUI.HideDetails();
        }

        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(true);
        }

        if (dragonMovement != null)
        {
            dragonMovement.CanMove = false;
        }

        if (dragonInteractor != null)
        {
            dragonInteractor.CanInteract = false;
        }

        if (toolController != null)
        {
            toolController.CanSwitchTools = false;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        RefreshInventory();
    }


    private void CloseInventory()
    {
        inventoryOpen = false;

        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        if (dragonMovement != null)
        {
            dragonMovement.CanMove = true;
        }

        if (dragonInteractor != null)
        {
            dragonInteractor.CanInteract = true;
        }

        if (toolController != null)
        {
            toolController.CanSwitchTools = true;
        }

        Cursor.visible = false;
    }


    private void RefreshInventory()
    {
        if (itemDetailsUI != null)
        {
            itemDetailsUI.HideDetails();
        }

        if (!inventoryOpen)
        {
            return;
        }

        if (
            dragonInventory == null ||
            slotContainer == null ||
            slotPrefab == null
        )
        {
            return;
        }


        // Delete the old UI slots.
        for (int i = slotContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(
                slotContainer.GetChild(i).gameObject
            );
        }


        // Create a UI slot for every inventory item.
        foreach (
            InventorySlot slot
            in dragonInventory.InventorySlots
        )
        {
            InventorySlotUI newSlot =
                Instantiate(
                    slotPrefab,
                    slotContainer
                );

            newSlot.Setup(slot, itemDetailsUI);
        }
    }
}