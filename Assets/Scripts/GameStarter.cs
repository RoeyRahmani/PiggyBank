using System;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    /// <summary>
    /// יש פונקציה שנקראת תחת הקלאס TimeAPI Servie והפונקציה נראת GET CUrrent Time
    /// היא תחזיר לך זמן בפורמט UTC ויהיא אסינכרוניט
    /// 
    /// Add save and load, 
    /// add current time, event duration, and start time
    /// build TimeAPIService
    /// </summary>

    public PiggyBankManager piggyBankManager;
    public GameObject mainMenuScreen;

    private bool isEventRunning = false;

    private void Start()
    {
        mainMenuScreen.SetActive(false);
    }

    public void TogglePiggyBankEventFromButton()
    {
        if (!isEventRunning)
        {
            // Turn ON the event
            int max = 1000;
            int current = 0;
            
            float fill = 0.5f;
            float timer = 300f;
            int totalCurreny = 0;
            

            mainMenuScreen.SetActive(true);
            piggyBankManager.InitializePiggyBank(max, current, fill, timer, totalCurreny);
            isEventRunning = true;

            Debug.Log("PiggyBankEvent Has Started!");
        }
        else
        {
            // Turn OFF the event
            mainMenuScreen.SetActive(false);
            piggyBankManager.StopPiggyBankEvent();
            isEventRunning = false;
        }
    }
}
