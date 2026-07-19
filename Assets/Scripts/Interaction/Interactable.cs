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

    public abstract void Interact();

}
