using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PiggyBankUIController : MonoBehaviour
{
    [Header("References")]
    // gameObject.SetActive(false);
    [SerializeField] private PiggyBankManager piggyBankManager;

    [Header("UI Elements")]
    [SerializeField] private GameObject piggyBankFullPopUp;
    [SerializeField] private GameObject piggyBankNotFullPopUp;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image coinProgressFillImage;
    [SerializeField] private GameObject piggyBankScreen;
    [SerializeField] private GameObject mainMenuScreen;


    [Header("BTN Elements")]
    [SerializeField] private Button openPiggyBankButton;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button addCoinsButton;
    [SerializeField] private Button unlockButton;

    private void Start()
    {
        openPiggyBankButton.onClick.AddListener(OpenPiggyBankScreen);
        backToMenuButton.onClick.AddListener(BackToMainMenu);
        addCoinsButton.onClick.AddListener(() => piggyBankManager.AddCoinsFromGold(1000));
        unlockButton.onClick.AddListener(() => piggyBankManager.UnlockPiggyBank());

    }

    private void OnEnable()
    {
        piggyBankManager.OnPiggyBankStarted += HandleEventStart;
        piggyBankManager.OnPiggyBankStopped += HandleEventStop;
        piggyBankManager.OnPiggyBankFull += ShowFullPopup;
        piggyBankManager.OnPiggyBankUnlocked += HandleUnlocked;
        piggyBankManager.OnCoinsUpdated += UpdateCoinUI;
        piggyBankManager.OnTimerUpdated += UpdateTimerUI;
        piggyBankManager.OnEventExpired += HandleExpired;
    }

    private void OnDisable()
    {
        piggyBankManager.OnPiggyBankStarted -= HandleEventStart;
        piggyBankManager.OnPiggyBankStopped -= HandleEventStop;
        piggyBankManager.OnPiggyBankFull -= ShowFullPopup;
        piggyBankManager.OnPiggyBankUnlocked -= HandleUnlocked;
        piggyBankManager.OnCoinsUpdated -= UpdateCoinUI;
        piggyBankManager.OnTimerUpdated -= UpdateTimerUI;
        piggyBankManager.OnEventExpired -= HandleExpired;
    }

    private void OpenPiggyBankScreen()
    {
        piggyBankNotFullPopUp.SetActive(true);
        piggyBankFullPopUp.SetActive(false);
        piggyBankScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }

    private void BackToMainMenu()
    {
        piggyBankScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    private void HandleEventStart()
    {
        //gameObject.SetActive(true);
        piggyBankNotFullPopUp.SetActive(true);
        piggyBankFullPopUp.SetActive(false);
    }

    private void HandleEventStop()
    {
        // Instead of disabling the whole controller, just hide the UI elements
        piggyBankNotFullPopUp.SetActive(false);
        piggyBankFullPopUp.SetActive(false);
        timerText.text = "";
        coinsText.text = "";
        totalCoinsText.text = "";

        if (coinProgressFillImage != null)
            coinProgressFillImage.fillAmount = 0;
    }

    private void UpdateCoinUI(int currentCoins, int totalCurrency)
    {
        coinsText.text = currentCoins + " / " + piggyBankManager.MaxCapacity;
        totalCoinsText.text = totalCurrency.ToString();

        if (coinProgressFillImage != null)
        // gameObject.SetActive(false);
        {
            float progress = (float)currentCoins / piggyBankManager.MaxCapacity;
            coinProgressFillImage.fillAmount = progress;
        }

        // Popup switching (if not full)
        if (!piggyBankManager.IsFull)
        {
            piggyBankNotFullPopUp.SetActive(true);
            piggyBankFullPopUp.SetActive(false);
        }
    }

    private void UpdateTimerUI(float remainingTime)
    {
        timerText.text = "Event ends in: " + Mathf.CeilToInt(remainingTime) + "s";
    }

    private void ShowFullPopup()
    {
        piggyBankFullPopUp.SetActive(true);
        piggyBankNotFullPopUp.SetActive(false);
    }

    private void HandleUnlocked()
    {
        Debug.Log("Piggy Bank Unlocked!");
        // Optionally reward player here
    }

    private void HandleExpired()
    {
        Debug.Log("Piggy Bank Event Expired");
        // Optionally show expired message
    }
}
