using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private string interactionPrompt = "Interact";

    public string InteractionPrompt
    {
        get
        {
            return interactionPrompt;
        }
    }

    protected void SetInteractionPrompt(string newPrompt)
    {
        interactionPrompt = newPrompt;
    }

    public abstract void Interact();
}