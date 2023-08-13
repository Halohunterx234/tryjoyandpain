using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotManager : MonoBehaviour
{
    [SerializeField]
    private GameObject itemButton;
    [SerializeField]
    private GameObject itemIcon;
    [SerializeField]
    private GameObject itemName;
    [SerializeField]
    private GameObject itemDesc;

    GameObject currentweapon;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        itemButton = this.gameObject.GetComponentsInChildren<Transform>()[1].gameObject;
        itemIcon = this.gameObject.GetComponentsInChildren<Transform>()[2].gameObject;
        itemName = this.gameObject.GetComponentsInChildren<Transform>()[3].gameObject;
        itemDesc = this.gameObject.GetComponentsInChildren<Transform>()[4].gameObject;
    }
    public void OnButtonClick()
    {
        inventoryManager.SelectWeapon(currentweapon);
    }

    public void SetData(GameObject weapon, bool pauseScreen=false)
    {
        this.gameObject.SetActive(true);
        currentweapon = weapon;
        Button button = itemButton.GetComponent<Button>();
        Image icon = itemIcon.GetComponent<Image>();
        Sprite sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        icon.sprite = sprite;
        TextMeshProUGUI name = itemName.GetComponent<TextMeshProUGUI>();
        name.text = weapon.name;
        TextMeshProUGUI description = itemDesc.GetComponent<TextMeshProUGUI>();
        if (weapon.GetComponent<WeaponController>())
        {
            WeaponController wc = weapon.GetComponent<WeaponController>();
            if (!pauseScreen)
            {
                if (wc.levelNum == 0)
                {
                    name.text += " 'NEW!'";
                }
                else if (wc.levelNum > 0)
                {
                    string nextLevel = (wc.levelNum + 1).ToString();
                    name.text += ": LVL " + nextLevel;
                }
                description.text = wc.levels[wc.levelNum].get_projectiles()[0].item_Desc;
            }
            else
            {
                name.text += ": LVL " + wc.levelNum;
                description.text = wc.levels[wc.levelNum].get_projectiles()[0].item_LevelDesc;
            }
        }
        else if (weapon.GetComponent<SupportSuperClass>())
        {
            SupportSuperClass ssc = weapon.GetComponent<SupportSuperClass>();
            if (!pauseScreen)
            {
                if (ssc.level == 0)
                {
                    name.text += " 'NEW!' ";
                }
                else if (ssc.level > 0)
                {
                    string nextLevel = (ssc.level + 1).ToString();
                    name.text += ": LVL " + nextLevel;
                }
                description.text = ssc.supportDescriptions[ssc.level];
            }
            else
            {
                name.text += ": LVL " + ssc.level;
                description.text = ssc.supportLevelDescriptions[ssc.level-1];
            }
        }
    }

    public void ClearData()
    {
        this.gameObject.SetActive(false);
    }
}
