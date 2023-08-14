using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookmarks : MonoBehaviour
{//this script is not used in the final product
    public GameObject titleScreen;

    public GameObject instructionScreen;
    public GameObject creditsScreen;
    public GameObject charSelectScreen;
    public GameObject settingsScreen;



    bool instructionsActive;
    bool creditsActive;
    bool charActive;
    bool settingsActive;

    public void closeAll()
    {
        titleScreen.SetActive(true);

        instructionsActive = false;
        instructionScreen.SetActive(false);
        creditsActive = false;
        creditsScreen.SetActive(false);
        charActive = false;
        charSelectScreen.SetActive(false);
        settingsActive = false;
        //settingsScreen.SetActive(false);
    }

    public void Instructions()
    {

        if (instructionsActive == false)
        {
            instructionScreen.SetActive(true);
            instructionsActive = true;

            //deactivate rest
            creditsActive = false;
            creditsScreen.SetActive(false);
            charActive = false;
            charSelectScreen.SetActive(false);
            settingsActive = false;
            //settingsScreen.SetActive(false);

            titleScreen.SetActive(false);
        }
        else closeAll();
    }

    public void Credits()
    {
        if (creditsActive == false)
        {
            instructionsActive = false;
            instructionScreen.SetActive(false);

            creditsActive = true;
            creditsScreen.SetActive(true);

            charActive = false;
            charSelectScreen.SetActive(false);
            settingsActive = false;
            //settingsScreen.SetActive(false);

            titleScreen.SetActive(false);
        }
        else closeAll();
    }

    public void CharSelect()
    {
        if (charActive == false)
        {
            instructionsActive = false;
            instructionScreen.SetActive(false);
            creditsActive = false;
            creditsScreen.SetActive(false);

            charActive = true;
            charSelectScreen.SetActive(true);

            settingsActive = false;
            //settingsScreen.SetActive(false);

            titleScreen.SetActive(false);
        }
        else closeAll();
    }

    public void Settings()
    {
        if (settingsActive == false)
        {
            instructionsActive = false;
            instructionScreen.SetActive(false);
            creditsActive = false;
            creditsScreen.SetActive(false);
            charActive = false;
            charSelectScreen.SetActive(false);

            settingsActive = true;
            //settingsScreen.SetActive(true);

            titleScreen.SetActive(false);
        }
        else closeAll();
    }


}
