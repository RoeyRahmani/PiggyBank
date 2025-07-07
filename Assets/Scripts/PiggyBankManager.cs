using System;
using UnityEngine;

public class PiggyBankManager : MonoBehaviour
{
    [Header("Core Values")]
    public int MaxCapacity { get; private set; }
    public int currentCoins { get; private set; }
    public float FillRate { get; private set; }
    public float EventDuration => eventTimer;
    private float eventTimer;

    private int totalPlayerCurrency;
    public bool IsFull => currentCoins >= MaxCapacity;
    public bool IsEventActive => eventTimer > 0f;

    // Events
    public event Action OnPiggyBankStarted;
    public event Action OnPiggyBankStopped;
    public event Action OnPiggyBankFull;
    public event Action OnPiggyBankUnlocked;
    public event Action OnEventExpired;
    public event Action<int, int> OnCoinsUpdated; // (currentCoins, totalCurrency)
    public event Action<float> OnTimerUpdated;    // (eventTimer remaining)

    private void Update()
    {
        if (!IsEventActive) return;

        eventTimer -= Time.deltaTime;
        OnTimerUpdated?.Invoke(eventTimer);

        if (eventTimer <= 0f)
        {
            eventTimer = 0f;
            OnEventExpired?.Invoke();
            StopPiggyBankEvent();
        }
    }

    public void InitializePiggyBank(int maxCapacity, int startingCoins, float fillRate, float timerDuration, int totalCurrency)
    {
        MaxCapacity = maxCapacity;
        currentCoins = startingCoins;
        FillRate = fillRate;
        eventTimer = timerDuration;
        totalPlayerCurrency = totalCurrency;

        OnPiggyBankStarted?.Invoke();
        OnCoinsUpdated?.Invoke(currentCoins, totalPlayerCurrency);
        OnTimerUpdated?.Invoke(eventTimer);

        Debug.Log("Piggy Bank Event Started");
    }

    public void StopPiggyBankEvent()
    {
        ResetPiggyBank();
        OnPiggyBankStopped?.Invoke();
        Debug.Log("Piggy Bank Event Stopped");
    }

    public void AddCoinsFromGold(int goldCollected)
    {
        totalPlayerCurrency += goldCollected;

        if (!IsEventActive || IsFull)
        {
            OnCoinsUpdated?.Invoke(currentCoins, totalPlayerCurrency);
            return;
        }

        int coinsToAdd = Mathf.FloorToInt(goldCollected * FillRate);
        currentCoins = Mathf.Min(currentCoins + coinsToAdd, MaxCapacity);

        if (IsFull)
        {
            OnPiggyBankFull?.Invoke();
        }

        OnCoinsUpdated?.Invoke(currentCoins, totalPlayerCurrency);
    }

    public void UnlockPiggyBank()
    {
        if (IsFull)
        {
            OnPiggyBankUnlocked?.Invoke();
        }
    }

    public void ResetPiggyBank()
    {
        currentCoins = 0;
        eventTimer = 0f;
    }

    // Optional Accessors
    public float GetRemainingTime() => eventTimer;
    public int GetTotalPlayerCurrency() => totalPlayerCurrency;
}
