using UnityEngine;

public class TestInteractable : Interactable
{
    [Header("Test Message")]

    [SerializeField] private string message = "The dragon gathered berries!";

    public override void Interact()
    {
        Debug.Log(message);
    }
}
