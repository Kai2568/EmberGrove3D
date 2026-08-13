using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Embergrove/Farming/Crop")]

public class CropData : ScriptableObject
{
    [Header("Crop Information")]
    [SerializeField] private string cropName;

    [Header("Items")]
    [SerializeField] private ItemData seedItem;
    [SerializeField] private ItemData harvestedItem;

    [Header("Growth Visuals")]
    [SerializeField] private Sprite seedSprite;
    [SerializeField] private Sprite stage1Sprite;
    [SerializeField] private Sprite stage2Sprite;
    [SerializeField] private Sprite stage3Sprite;

    [Header("Growth Settings")]
    [SerializeField] private float stage1Time = 3f;
    [SerializeField] private float stage2Time = 3f;
    [SerializeField] private float stage3Time = 3f;

    [Header("Harvest")]
    [SerializeField] private int harvestAmount = 1;

    public string CropName => cropName;

    public ItemData SeedItem => seedItem;

    public ItemData HarvestedItem => harvestedItem;

    public Sprite SeedSprite => seedSprite;

    public Sprite Stage1Sprite => stage1Sprite;

    public Sprite Stage2Sprite => stage2Sprite;

    public Sprite Stage3Sprite => stage3Sprite;

    public float Stage1Time => stage1Time;

    public float Stage2Time => stage2Time;

    public float Stage3Time => stage3Time;

    public int HarvestAmount => harvestAmount;

}
