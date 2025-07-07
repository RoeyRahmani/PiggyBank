using System;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public PiggyBankManager piggyBankManager;
    public GameObject mainMenuScreen;
    public GameObject piggyBankScreen;

    private void Start()
    {
        // Both screens off at start
        mainMenuScreen.SetActive(false);
        piggyBankScreen.SetActive(false);
    }

    public void TogglePiggyBankEventFromButton()
    {
        if (!piggyBankManager.IsEventActive)
        {
            int max = 1000;
            int current = 0;
            float fill = 0.5f;
            float timer = 300f;
            int totalCurrency = 0;

            mainMenuScreen.SetActive(true);
            piggyBankScreen.SetActive(false);

            piggyBankManager.InitializePiggyBank(max, current, fill, timer, totalCurrency);

            Debug.Log("PiggyBankEvent Has Started!");
        }
        else
        {
            mainMenuScreen.SetActive(false);
            piggyBankScreen.SetActive(false);

            piggyBankManager.StopPiggyBankEvent();

            Debug.Log("PiggyBankEvent Has Stopped!");
        }
    }
}
