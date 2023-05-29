using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D;
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

    public void SetData(GameObject weapon)
    {
        currentweapon = weapon;
        WeaponController wc = weapon.GetComponent<WeaponController>();
        Button button = itemButton.GetComponent<Button>();
        Image icon = itemIcon.GetComponent<Image>();
        Sprite sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        icon.sprite = sprite;
        TextMeshProUGUI name = itemName.GetComponent<TextMeshProUGUI>();
        name.text = wc.gameObject.name;
        if (wc.levelNum == 0)
        {
            name.text += " 'NEW'" ;
        }
        else if (wc.levelNum > 0)
        {
            name.text += ": " + wc.levelNum;
        }
    }
}
