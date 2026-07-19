using System.Collections;
using UnityEngine;

public class BerryBush : Interactable
{
    [Header("Berry Settings")]
    [SerializeField] private int berryAmount = 3;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnTime = 5f;

    [Header("References")]
    [SerializeField] private GameObject bushVisual;
    [SerializeField] private Collider bushCollider;

    private DragonInventory dragonInventory;
    private bool canGather = true;

    private void Start()
    {
        dragonInventory = FindFirstObjectByType<DragonInventory>();
    }

    public override void Interact()
    {
        if (!canGather)
        {
            return;
        }

        if (dragonInventory == null)
        {
            Debug.LogWarning("No DragonInventory was found in the scene.");

            return;
        }

        dragonInventory.AddBerries(berryAmount);

        StartCoroutine(RespawnBush());
    }

    private IEnumerator RespawnBush()
    {
        canGather = false;

        if (bushVisual != null)
        {
            bushVisual.SetActive(false);
        }

        if (bushCollider != null)
        {
            bushCollider.enabled = false;
        }

        yield return new WaitForSeconds(respawnTime);

        if (bushVisual != null)
        {
            bushVisual.SetActive(true);
        }

        if (bushCollider != null)
        {
            bushCollider.enabled = true;
        }

        canGather = true;
    }
}
