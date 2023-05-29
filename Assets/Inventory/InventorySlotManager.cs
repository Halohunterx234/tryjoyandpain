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
        icon.enabled = false;
    }
    public void SetIcon(GameObject weapon)
    {
        icon.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        icon.enabled = true;
    }
}