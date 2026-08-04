using System.Collections;
using UnityEngine;

public class FarmingPlot : Interactable
{
    private enum PlotState
    {
        Unprepared,
        Tilled,
        NeedsWater,
        Growing,
        Ready
    }

    [Header("Items")]
    [SerializeField] private ItemData seedItem;
    [SerializeField] private ItemData harvestedItem;

    [Header("Crop Settings")]
    [SerializeField] private int seedsRequired = 1;
    [SerializeField] private int harvestAmount = 1;
    [SerializeField] private float growthTime = 5f;

    [Header("Visuals")]
    [SerializeField] private GameObject unpreparedVisual;
    [SerializeField] private GameObject tilledVisual;
    [SerializeField] private GameObject plantedVisual;
    [SerializeField] private GameObject wateredVisual;
    [SerializeField] private GameObject grownVisual;

    private DragonInventory dragonInventory;
    private DragonToolController toolController;

    private PlotState currentState = PlotState.Unprepared;
    private Coroutine growthCoroutine;

    private void Start()
    {
        dragonInventory =
            FindFirstObjectByType<DragonInventory>();

        toolController =
            FindFirstObjectByType<DragonToolController>();

        UpdatePlot();
    }

    public override void Interact()
    {
        if (dragonInventory == null)
        {
            Debug.LogWarning(
                "No DragonInventory was found in the scene."
            );

            return;
        }

        if (toolController == null)
        {
            Debug.LogWarning(
                "No DragonToolController was found in the scene."
            );

            return;
        }

        switch (currentState)
        {
            case PlotState.Unprepared:
                TryTillSoil();
                break;

            case PlotState.Tilled:
                TryPlantSeed();
                break;

            case PlotState.NeedsWater:
                TryWaterCrop();
                break;

            case PlotState.Growing:
                Debug.Log("The crop is still growing.");
                break;

            case PlotState.Ready:
                TryHarvestCrop();
                break;
        }
    }

    private void TryTillSoil()
    {
        if (!toolController.IsUsingTool(ToolType.Hoe))
        {
            Debug.Log(
                "Equip the Hoe before tilling the soil."
            );

            return;
        }

        currentState = PlotState.Tilled;

        Debug.Log("The soil has been tilled.");

        UpdatePlot();
    }

    private void TryPlantSeed()
    {
        if (!toolController.IsUsingTool(ToolType.Hands))
        {
            Debug.Log(
                "Use your Hands to plant the seed."
            );

            return;
        }

        if (seedItem == null)
        {
            Debug.LogWarning(
                "The seed item has not been assigned."
            );

            return;
        }

        bool removedSeed =
            dragonInventory.RemoveItem(
                seedItem,
                seedsRequired
            );

        if (!removedSeed)
        {
            Debug.Log(
                "You need "
                + seedsRequired
                + " "
                + seedItem.ItemName
                + "."
            );

            return;
        }

        currentState = PlotState.NeedsWater;

        Debug.Log(
            seedItem.ItemName
            + " planted. The crop needs water."
        );

        UpdatePlot();
    }

    private void TryWaterCrop()
    {
        if (!toolController.IsUsingTool(ToolType.WateringCan))
        {
            Debug.Log(
                "Equip the Watering Can before watering."
            );

            return;
        }

        currentState = PlotState.Growing;

        UpdatePlot();

        if (growthCoroutine != null)
        {
            StopCoroutine(growthCoroutine);
        }

        growthCoroutine = StartCoroutine(GrowCrop());

        Debug.Log("The crop has been watered.");
    }

    private IEnumerator GrowCrop()
    {
        yield return new WaitForSeconds(growthTime);

        currentState = PlotState.Ready;
        growthCoroutine = null;

        UpdatePlot();

        if (harvestedItem != null)
        {
            Debug.Log(
                harvestedItem.ItemName
                + " is ready to harvest."
            );
        }
    }

    private void TryHarvestCrop()
    {
        if (!toolController.IsUsingTool(ToolType.Hands))
        {
            Debug.Log(
                "Use your Hands to harvest the crop."
            );

            return;
        }

        if (harvestedItem == null)
        {
            Debug.LogWarning(
                "The harvested item has not been assigned."
            );

            return;
        }

        dragonInventory.AddItem(
            harvestedItem,
            harvestAmount
        );

        currentState = PlotState.Unprepared;

        Debug.Log(
            "Harvested "
            + harvestAmount
            + " "
            + harvestedItem.ItemName
            + "."
        );

        UpdatePlot();
    }

    private void UpdatePlot()
    {
        SetVisualActive(
            unpreparedVisual,
            currentState == PlotState.Unprepared
        );

        SetVisualActive(
            tilledVisual,
            currentState == PlotState.Tilled
        );

        SetVisualActive(
            plantedVisual,
            currentState == PlotState.NeedsWater
        );

        SetVisualActive(
            wateredVisual,
            currentState == PlotState.Growing
        );

        SetVisualActive(
            grownVisual,
            currentState == PlotState.Ready
        );

        UpdatePrompt();
    }

    private void SetVisualActive(
        GameObject visual,
        bool shouldBeActive
    )
    {
        if (visual != null)
        {
            visual.SetActive(shouldBeActive);
        }
    }

    private void UpdatePrompt()
    {
        switch (currentState)
        {
            case PlotState.Unprepared:
                SetInteractionPrompt("Till Soil");
                break;

            case PlotState.Tilled:
                SetInteractionPrompt(
                    seedItem != null
                        ? "Plant " + seedItem.ItemName
                        : "Plant Seed"
                );
                break;

            case PlotState.NeedsWater:
                SetInteractionPrompt("Water Crop");
                break;

            case PlotState.Growing:
                SetInteractionPrompt("Growing...");
                break;

            case PlotState.Ready:
                SetInteractionPrompt(
                    harvestedItem != null
                        ? "Harvest " + harvestedItem.ItemName
                        : "Harvest Crop"
                );
                break;
        }
    }
}