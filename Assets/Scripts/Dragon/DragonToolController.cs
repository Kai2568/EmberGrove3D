using System;
using UnityEngine;

public class DragonToolController : MonoBehaviour
{
    [Header("Current Tool")]
    [SerializeField] private ToolType currentTool = ToolType.Hands;

    public bool CanSwitchTools { get; set; } = true;

    public event Action<ToolType> ToolChanged;

    public ToolType CurrentTool
    {
        get
        {
            return currentTool;
        }
    }

    private void Start()
    {
        Debug.Log(
            "Selected tool: " + GetToolDisplayName(currentTool)
        );

        ToolChanged?.Invoke(currentTool);
    }

    private void Update()
    {
        ReadToolInput();
    }

    private void ReadToolInput()
    {
        if (!CanSwitchTools)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectTool(ToolType.Hands);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectTool(ToolType.Hoe);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectTool(ToolType.WateringCan);
        }
    }

    public void SelectTool(ToolType newTool)
    {
        if (currentTool == newTool)
        {
            return;
        }

        currentTool = newTool;

        Debug.Log(
            "Selected tool: " + GetToolDisplayName(currentTool)
        );

        ToolChanged?.Invoke(currentTool);
    }

    public bool IsUsingTool(ToolType requiredTool)
    {
        return currentTool == requiredTool;
    }

    public string GetCurrentToolDisplayName()
    {
        return GetToolDisplayName(currentTool);
    }

    private string GetToolDisplayName(ToolType tool)
    {
        switch (tool)
        {
            case ToolType.Hands:
                return "Hands";

            case ToolType.Hoe:
                return "Hoe";

            case ToolType.WateringCan:
                return "Watering Can";

            default:
                return tool.ToString();
        }
    }
}