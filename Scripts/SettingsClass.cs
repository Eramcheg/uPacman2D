using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Trida slouzi k tomu aby zahrnuji interface MENU a menit aktualni buttony.



public class SettingsClass : MonoBehaviour
{
    private List<string> algorithms;
    private int counter = -1;
    public GameObject panel;
    public GameObject menu;

    public GameObject settings;
    public GameObject rules;
    public GameObject MenuBtn;

    public GameManager_ gameManager_;
    
    public void ChangeAlgorithm()
    {
        MenuBtn.SetActive(false);
        panel.SetActive(true);
       
        menu.SetActive(true);
        Time.timeScale = 0;
        //counter = (counter + 1) % algorithms.Count;
        
       //return algorithms[counter];
    }
    

    //Tlacitko resume
    public void ResumeGame()
    {
        Time.timeScale = 1;
        MenuBtn.SetActive(true);
        panel.SetActive(false);
        menu.SetActive(false);

    }

    //Tlacitko Settings
    public void SettingsGame()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }


    //Tlacitko Rules
    public void RulesGame()
    {
        rules.SetActive(true);
        menu.SetActive(false);
    }


    //Tlacitko Back
    public void BackBtn()
    {
        menu.SetActive(true);
        settings.SetActive(false);
        rules.SetActive(false);
    }


    //Tlacitko Quit
    public void QuitGame()
    {
        Application.Quit();
    }
   
}
