using UnityEngine;

public class BerryBush : Interactable
{
    [Header("Berry Settings")]
    [SerializeField] private int berryAmount = 3;

    private DragonInventory dragonInventory;

    private void Start()
    {
        dragonInventory = FindFirstObjectByType<DragonInventory>();
    }

    public override void Interact()
    {
        if (dragonInventory == null)
        {
            Debug.LogWarning("No DragonInventory was found in the scene.");

            return;
        }

        dragonInventory.AddBerries(berryAmount);

        gameObject.SetActive(false);
    }
}
