using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XpController : MonoBehaviour
{
    //to be attached to the xp bar
    //is to control the xp levels of the player
    [SerializeField]
    private int xplvl, xp, xpMax;
    Slider xpBar;
    TextMeshProUGUI xplvltext;
    Arua_Damage ar;
    InventoryManager im;

    private void Awake()
    {
        xp = 0;
        xplvl = 1;
        xpMax = CalculateNextLvlXP(xplvl, xp);
        ar = FindObjectOfType<Arua_Damage>();
        im = FindObjectOfType<InventoryManager>();
    }
    private void Start()
    {
        xpBar = GetComponent<Slider>();
        xpBar.maxValue = xpMax; xpBar.minValue = xp; xpBar.value = xp;
        xplvltext = GameObject.Find("XpLvl").GetComponent<TextMeshProUGUI>();
        xplvltext.text = xplvl.ToString();
    }
    private void Update()
    {
        if (xplvl == 2 && !ar.isOn)
        {
            ar.isOn = true;
        }
    }
    int CalculateNextLvlXP(int lvl, int xp)
    {
        //formula
        return Mathf.RoundToInt(lvl*5+Mathf.Log10(Mathf.Pow(lvl, 2)))+xp;
    }

    public void AddXP(int newXP)
    {
        xp += newXP;
        if (xp >= xpMax)
        {
            xplvl++;
            xpMax = CalculateNextLvlXP(xplvl, xp);
            xpBar.maxValue = xpMax; xpBar.minValue = xp;
            xplvltext.text = xplvl.ToString();
            im.SpawnWeapons();
            //play sound effect

        }
        xpBar.value = xp;
    }

}
