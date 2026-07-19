using UnityEngine;

public class DragonInteractor : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private DragonMovement dragonMovement;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 1.5f;
    [SerializeField] private float interactionRadius = 0.4f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("Debug")]
    [SerializeField] private bool showDebugMessages = true;

    private Interactable currentInteractable;
    private Interactable previousInteractable;

    private void Update()
    {
        FindInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void FindInteractable()
    {
        currentInteractable = null;

        if (dragonMovement == null || interactionPoint == null)
        {
            return;
        }

        Vector3 direction = dragonMovement.FacingDirection;

        RaycastHit hit;

        bool foundObject = Physics.SphereCast(
            interactionPoint.position,
            interactionRadius,
            direction,
            out hit,
            interactionDistance,
            interactableLayer
            );

        if (foundObject)
        {
            currentInteractable = hit.collider.GetComponentInParent<Interactable>();
        }

        CheckForInteractionChange();
    }

    private void CheckForInteractionChange()
    {
        if (currentInteractable == previousInteractable)
        {
            return;
        }

        if (currentInteractable != null)
        {
            if (interactionPromptUI != null)
            {
                interactionPromptUI.ShowPrompt(currentInteractable.InteractionPrompt);
            }

            if (showDebugMessages)
            {
                Debug.Log("Nearby interaction: " + currentInteractable.InteractionPrompt);
            }
        }

        else
        {
            if (interactionPromptUI != null)
            {
                interactionPromptUI.HidePrompt();
            }

            if (showDebugMessages && previousInteractable != null)
            {
                Debug.Log("No longer near an interactable object");
            }
        }

        previousInteractable = currentInteractable;
    }

    private void TryInteract()
    {
        if (currentInteractable == null)
        {
            if (showDebugMessages)
            {
                Debug.Log("There is nothing to interact with.");
            }

            return;
        }

        currentInteractable.Interact();
    }

    private void OnDisable()
    {
        if (interactionPromptUI != null)
        {
            interactionPromptUI.HidePrompt();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionPoint == null)
        {
            return;
        }

        Vector3 direction = Vector3.forward;

        if (dragonMovement != null)
        {
            direction = dragonMovement.FacingDirection;
        }

        Vector3 endPosition = interactionPoint.position + direction * interactionDistance;

        Gizmos.DrawLine(
            interactionPoint.position,
            endPosition
            );

        Gizmos.DrawWireSphere(
            endPosition,
            interactionRadius
            );
    }
}
