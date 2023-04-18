using UnityEngine;

namespace Core.Models
{
internal static class InventoryHelper
{
    public static void LogPutItemResults(int successfullyPutAmount, int targetAmount, string itemId)
    {
        if (successfullyPutAmount == 0)
        {
            Debug.LogError($"Can't put '{itemId}' into inventory");
            return;
        }

        if (successfullyPutAmount < targetAmount)
        {
            Debug.LogError($"Managed to fit only {successfullyPutAmount} of {targetAmount} instances of item '{itemId}'");
            return;
        }

        Debug.Log($"Item '{itemId}' was successfully placed into inventory");
    }
}
}