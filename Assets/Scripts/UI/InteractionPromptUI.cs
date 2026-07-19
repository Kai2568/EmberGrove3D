using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private TMP_Text promptText;

    private void Awake()
    {
        HidePrompt();
    }

    public void ShowPrompt(string message)
    {
        if (promptPanel == null || promptText == null)
        {
            return;
        }

        promptText.text = "[E] " + message;
        promptPanel.SetActive(true);
    }

    public void HidePrompt()
    {
        if (promptPanel == null)
        {
            return;
        }

        promptPanel.SetActive(false);
    }
}
