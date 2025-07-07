using System;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public PiggyBankManager piggyBankManager;
    public GameObject mainMenuScreen;
    public GameObject piggyBankScreen; // Add this reference

    private bool isEventRunning = false;

    private void Start()
    {
        // Both screens off at start
        mainMenuScreen.SetActive(false);
        piggyBankScreen.SetActive(false);
    }

    public void TogglePiggyBankEventFromButton()
    {
        if (!isEventRunning)
        {
            int max = 1000;
            int current = 0;
            float fill = 0.5f;
            float timer = 300f;
            int totalCurreny = 0;

            // Show only main menu when event starts
            mainMenuScreen.SetActive(true);
            piggyBankScreen.SetActive(false);

            piggyBankManager.InitializePiggyBank(max, current, fill, timer, totalCurreny);
            isEventRunning = true;

            Debug.Log("PiggyBankEvent Has Started!");
        }
        else
        {
            mainMenuScreen.SetActive(false);
            piggyBankScreen.SetActive(false);

            piggyBankManager.StopPiggyBankEvent();
            isEventRunning = false;
        }
    }
}
public class GameManager : MonoBehaviour
{
    public GameObject mainMenuScreenUI;
    public GameObject piggyBankScreenUI;

    void Start()
    {
        // Both screens off at start
        mainMenuScreenUI.SetActive(false);
        piggyBankScreenUI.SetActive(false);
    }

    public void ShowPiggyBankScreen()
    {
        piggyBankScreenUI.SetActive(true);
        mainMenuScreenUI.SetActive(false);
    }

    public void BackToMainMenuBTN()
    {
        mainMenuScreenUI.SetActive(true);
        piggyBankScreenUI.SetActive(false);
    }
}
