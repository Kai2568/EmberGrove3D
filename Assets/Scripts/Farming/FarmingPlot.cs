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

    [Header("Growth Times")]
    [SerializeField] private float stage1Time = 3f;
    [SerializeField] private float stage2Time = 3f;
    [SerializeField] private float stage3Time = 3f;

    [Header("Soil Visuals")]
    [SerializeField] private GameObject unpreparedVisual;
    [SerializeField] private GameObject tilledVisual;

    [Header("Crop Visuals")]
    [SerializeField] private GameObject seedVisual;
    [SerializeField] private GameObject cropStage1;
    [SerializeField] private GameObject cropStage2;
    [SerializeField] private GameObject cropStage3;

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

        UpdateVisuals();
        UpdatePrompt();
    }


    public override void Interact()
    {
        if (dragonInventory == null)
        {
            Debug.LogWarning(
                "No DragonInventory was found."
            );

            return;
        }

        if (toolController == null)
        {
            Debug.LogWarning(
                "No DragonToolController was found."
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

                Debug.Log(
                    "The carrot is still growing."
                );

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
                "Equip the Hoe to till this soil."
            );

            return;
        }


        currentState = PlotState.Tilled;

        Debug.Log(
            "The soil has been tilled."
        );

        UpdateVisuals();
        UpdatePrompt();
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
                "No seed item has been assigned."
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
                "You don't have enough "
                + seedItem.ItemName
                + "."
            );

            return;
        }


        currentState = PlotState.NeedsWater;

        Debug.Log(
            seedItem.ItemName
            + " planted."
        );


        UpdateVisuals();
        UpdatePrompt();
    }


    private void TryWaterCrop()
    {
        if (
            !toolController.IsUsingTool(
                ToolType.WateringCan
            )
        )
        {
            Debug.Log(
                "Equip the Watering Can first."
            );

            return;
        }


        currentState = PlotState.Growing;

        UpdateVisuals();
        UpdatePrompt();


        if (growthCoroutine != null)
        {
            StopCoroutine(growthCoroutine);
        }


        growthCoroutine =
            StartCoroutine(
                GrowCrop()
            );


        Debug.Log(
            "The carrot has been watered."
        );
    }


    private IEnumerator GrowCrop()
    {
        // -------------------------
        // GROWTH STAGE 1
        // -------------------------

        ShowCropStage(1);

        Debug.Log(
            "Carrot entered Growth Stage 1."
        );


        yield return new WaitForSeconds(
            stage1Time
        );


        // -------------------------
        // GROWTH STAGE 2
        // -------------------------

        ShowCropStage(2);

        Debug.Log(
            "Carrot entered Growth Stage 2."
        );


        yield return new WaitForSeconds(
            stage2Time
        );


        // -------------------------
        // GROWTH STAGE 3
        // -------------------------

        ShowCropStage(3);

        Debug.Log(
            "Carrot entered Growth Stage 3."
        );


        yield return new WaitForSeconds(
            stage3Time
        );


        // -------------------------
        // READY TO HARVEST
        // -------------------------

        currentState = PlotState.Ready;

        growthCoroutine = null;


        UpdateVisuals();
        UpdatePrompt();


        Debug.Log(
            "The carrot is ready to harvest!"
        );
    }


    private void TryHarvestCrop()
    {
        if (!toolController.IsUsingTool(ToolType.Hands))
        {
            Debug.Log(
                "Use your Hands to harvest the carrot."
            );

            return;
        }


        if (harvestedItem == null)
        {
            Debug.LogWarning(
                "No harvested item has been assigned."
            );

            return;
        }


        dragonInventory.AddItem(
            harvestedItem,
            harvestAmount
        );


        Debug.Log(
            "Harvested "
            + harvestAmount
            + " "
            + harvestedItem.ItemName
            + "."
        );


        // Keep the soil tilled after harvesting.
        currentState = PlotState.Tilled;


        UpdateVisuals();
        UpdatePrompt();
    }


    private void ShowCropStage(int stage)
    {
        SetVisualActive(
            seedVisual,
            false
        );

        SetVisualActive(
            cropStage1,
            stage == 1
        );

        SetVisualActive(
            cropStage2,
            stage == 2
        );

        SetVisualActive(
            cropStage3,
            stage == 3
        );
    }


    private void UpdateVisuals()
    {
        // -------------------------
        // SOIL
        // -------------------------

        SetVisualActive(
            unpreparedVisual,
            currentState == PlotState.Unprepared
        );


        SetVisualActive(
            tilledVisual,
            currentState != PlotState.Unprepared
        );


        // -------------------------
        // CROP
        // -------------------------

        if (currentState == PlotState.Unprepared ||
            currentState == PlotState.Tilled)
        {
            HideAllCropVisuals();
        }


        if (currentState == PlotState.NeedsWater)
        {
            HideAllCropVisuals();

            SetVisualActive(
                seedVisual,
                true
            );
        }


        if (currentState == PlotState.Ready)
        {
            HideAllCropVisuals();

            SetVisualActive(
                cropStage3,
                true
            );
        }
    }


    private void HideAllCropVisuals()
    {
        SetVisualActive(
            seedVisual,
            false
        );

        SetVisualActive(
            cropStage1,
            false
        );

        SetVisualActive(
            cropStage2,
            false
        );

        SetVisualActive(
            cropStage3,
            false
        );
    }


    private void SetVisualActive(
        GameObject visual,
        bool active
    )
    {
        if (visual != null)
        {
            visual.SetActive(active);
        }
    }


    private void UpdatePrompt()
    {
        switch (currentState)
        {
            case PlotState.Unprepared:

                SetInteractionPrompt(
                    "Till Soil"
                );

                break;


            case PlotState.Tilled:

                SetInteractionPrompt(
                    seedItem != null
                        ? "Plant " + seedItem.ItemName
                        : "Plant Seed"
                );

                break;


            case PlotState.NeedsWater:

                SetInteractionPrompt(
                    "Water Carrot"
                );

                break;


            case PlotState.Growing:

                SetInteractionPrompt(
                    "Carrot Growing..."
                );

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