using UnityEngine;

public class PiggyBankRewardManager : MonoBehaviour
{
    public PiggyBankManager piggyBank;
    public int MultiplyFactor = 2; // RV button multiplier

    private bool eventExpired = false;

    public void ClaimReward(bool watchedRV)
    {
        if (!piggyBank.IsEventActive())
        {
            // Fallback claim after expiry
            PartialClaimWithRV();
            return;
        }

        int reward = piggyBank.CurrentCoins;
        if (watchedRV)
            reward *= MultiplyFactor;

        UpdatePlayerCoins(reward);
        piggyBank.ResetPiggyBank();
    }

    private void PartialClaimWithRV()
    {
        if (!eventExpired)
        {
            int partialReward = Mathf.FloorToInt(piggyBank.CurrentCoins * 0.5f); // 50% Reward
            UpdatePlayerCoins(partialReward);
            eventExpired = true;
        }
    }

    private void UpdatePlayerCoins(int amount)
    {
        //PlayerData.TotalCoins += amount;
        Debug.Log($"Player received {amount} coins.");
    }
}
