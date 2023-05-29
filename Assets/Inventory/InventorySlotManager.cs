using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotManager : MonoBehaviour
{
    public Image icon;
    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        print(icon);
        icon.enabled = false;
        print(icon);
    }
    public void SetIcon(GameObject weapon)
    {
        print(icon);
        print(weapon);
        icon.enabled = true;
        print(icon);
        icon.sprite = weapon.GetComponent<SpriteRenderer>().sprite;

    }
}
