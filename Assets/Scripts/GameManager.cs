using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuScreenUI;
    public GameObject piggyBankScreenUI;// 

    // test v2



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuScreenUI.SetActive(true); 
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
