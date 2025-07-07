using UnityEngine;

public class PiggyBankRewardManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PiggyBankManager piggyBank;

    [Header("Reward Settings")]
    [SerializeField] private int multiplyFactor = 2; // Multiplier for RV

    private bool canPartialClaim = false;

    private void OnEnable()
    {
        if (piggyBank != null)
            piggyBank.OnEventExpired += EnablePartialClaim;
    }

    private void OnDisable()
    {
        if (piggyBank != null)
            piggyBank.OnEventExpired -= EnablePartialClaim;
    }

    public void ClaimReward(bool watchedRV)
    {
        int reward = 0;

        if (piggyBank.IsEventActive)
        {
            reward = piggyBank.currentCoins;
            if (watchedRV)
                reward *= multiplyFactor;

            piggyBank.ResetPiggyBank();
        }
        else if (canPartialClaim)
        {
            reward = Mathf.FloorToInt(piggyBank.currentCoins * 0.5f);
            canPartialClaim = false;
        }
        else
        {
            Debug.Log("Event is not active and partial reward has already been claimed.");
            return;
        }

        UpdatePlayerCoins(reward);
    }

    private void EnablePartialClaim()
    {
        canPartialClaim = true;
        Debug.Log("Event expired: partial reward claim enabled.");
    }

    private void UpdatePlayerCoins(int amount)
    {
        // e.g. PlayerData.Instance.AddCoins(amount);
        Debug.Log($"Player received {amount} coins.");
    }
}
