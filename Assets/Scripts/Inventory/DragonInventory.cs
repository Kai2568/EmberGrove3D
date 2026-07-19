using UnityEngine;

public class DragonInventory : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private int berryCount;

    public int BerryCount
    {
        get
        {
            return berryCount;
        }
    }

    public void AddBerries(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        berryCount += amount;

        Debug.Log(
            "Berries Collected: "
            + amount
            + " | Total berries: "
            + berryCount
            );
    }
}
