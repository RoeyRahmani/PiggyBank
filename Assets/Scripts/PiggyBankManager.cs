using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PiggyBankManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// check if we can delete isEventActive and change it to Events
    /// 
    /// </summary>

    [Header("Screens")]
    public GameObject piggyBankNotFullScreen;
    public GameObject piggyBankFullScreen;
    public GameObject piggyBankScreen;

    //public Slider coinProgressBar;
    [Header("Texts")]
    public TextMeshProUGUI CurrentCoinsText;
    public TextMeshProUGUI totalPlayerCurrencyText;
    public TextMeshProUGUI eventTimeText;

    [Header("Values")]
    int MaxCapacity; // **get this from datebase
    public int CurrentCoins { get; private set; } // **get this from datebase
    private int totalPlayerCurrency;
    public float FillRate;
    public bool IsFull => CurrentCoins >= MaxCapacity; // change to bool void. and check if changed
    private float eventTimer; // **get this from datebase
    public bool isEventActive = false;

    public event Action OnPiggyBankFull;
    public event Action OnPiggyBankUnlocked;
    public event Action OnEventExpired;

    [Header("Progress Bar")]
    public Image coinProgressFillImage;

    void Update()
    {
        if (!isEventActive) return;
        UpdateUI();

        eventTimer -= Time.deltaTime;
        if (eventTimer <= 0)
        {
            isEventActive = false;
            OnEventExpired?.Invoke();
            ResetPiggyBank();
            
        } 
    }
    private void UpdateUI()
    {
        if (!isEventActive)
        {
            piggyBankFullScreen.SetActive(false);
            piggyBankNotFullScreen.SetActive(false);
            totalPlayerCurrencyText.text = "";
            CurrentCoinsText.text = "";
            eventTimeText.text = "";

            if (coinProgressFillImage != null)
                coinProgressFillImage.fillAmount = 0;

            return;
        }

        if (IsFull)
        {
            CurrentCoinsText.text = CurrentCoins.ToString();
            piggyBankFullScreen.SetActive(true);
            piggyBankNotFullScreen.SetActive(false);
        }
        else
        {
            CurrentCoinsText.text = CurrentCoins.ToString() + " / " + MaxCapacity.ToString();
            piggyBankFullScreen.SetActive(false);
            piggyBankNotFullScreen.SetActive(true);
        }

        eventTimeText.text = "Event ends in: " + Mathf.CeilToInt(eventTimer).ToString() + "s";
        totalPlayerCurrencyText.text = totalPlayerCurrency.ToString();

        if (coinProgressFillImage != null)
        {
            coinProgressFillImage.fillAmount = (float)CurrentCoins / MaxCapacity;
        }
    }

    public void InitializePiggyBank(int maxCapacity, int startingCoins, float fillRate, float timerDuration, int totalCurrency)
    {

        MaxCapacity = maxCapacity;
        CurrentCoins = startingCoins;
        FillRate = fillRate;
        eventTimer = timerDuration;
        totalPlayerCurrency = totalCurrency;

        isEventActive = true;
        Debug.Log("event has started");
        UpdateUI();
    }
    public void StopPiggyBankEvent()
    {
        isEventActive = false;
        ResetPiggyBank();
        
        piggyBankScreen.SetActive(false);
        CurrentCoinsText.text = "";
        eventTimeText.text = "";
        totalPlayerCurrencyText.text = "";


        if (coinProgressFillImage != null)
            coinProgressFillImage.fillAmount = 0;

        Debug.Log("PiggyBankEvent Has Stopped!");
    }

    public void AddCoinsFromGold(int goldCollected)
    {
        totalPlayerCurrency += goldCollected;

        if (!isEventActive || IsFull)
        {
            UpdateUI(); // So the currency still updates even if piggy is full or event is off
            return;
        }

        int coinsToAdd = Mathf.FloorToInt(goldCollected * FillRate);
        CurrentCoins = Mathf.Min(CurrentCoins + coinsToAdd, MaxCapacity);

        if (IsFull)
        {
            OnPiggyBankFull?.Invoke();
        }

        UpdateUI();
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
        CurrentCoins = 0;
        eventTimer = 0;
    }

    public void AddCoins()
    {
        AddCoinsFromGold(1000);
    }
    public bool IsFulls()
    {
        return false;
    }
    public bool IsEventActive() => isEventActive;

    public float GetRemainingTime() => eventTimer;
}
