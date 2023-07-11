using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookFlip : MonoBehaviour
{
    public GameObject book;
    public GameObject bookFlipping;
    public GameObject playerHP;
    public MenuController mc;

    //data
    public Player player;
    public Modifiers itemMod;
    public WaveController wc;
    public XpController xpC;

    public GameObject TimeTxt, Level, Health, CD, Speed, LifeRegen;

    //text

    // Start is called before the first frame update
    public void deactivate()
    {
        book.SetActive(true);
        SetData();
        bookFlipping.SetActive(false);
    } 

    public void activate()
    {
        playerHP.SetActive(false);
        bookFlipping.SetActive(true);
        book.SetActive(false);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        playerHP.SetActive(true);
        book.SetActive(false);
    }
    
    public void SetData()
    {
        TimeTxt.GetComponent<Text>().text = wc.time_text_string;
        Level.GetComponent<Text>().text = xpC.xplvl.ToString();
        Health.GetComponent<Text>().text = player.hp.ToString();
        CD.GetComponent<Text>().text = "-0."+itemMod.cdModifier.ToString()+"x";
        Speed.GetComponent<Text>().text = "+0."+itemMod.speedModifier.ToString()+"x";
        LifeRegen.GetComponent<Text>().text = itemMod.lifeRegenAmtModifier.ToString();
        Time.timeScale = 0; 
    }
}
