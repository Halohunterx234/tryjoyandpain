using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> weaponSlots;
    public List<GameObject> supportSlots;
    public List<GameObject> items;
    public Dictionary<GameObject, GameObject> weaponPair = new Dictionary<GameObject, GameObject>();

    [Header("GUI")]
    public GameObject levelGUI;
    public GameObject inventory;
    public List<GameObject> itemSlots;
    [SerializeField]
    private List<GameObject> gacha_itemList;
    public List<GameObject> inventorySlots;   

    float i;
    private void Awake()
    {
        levelGUI.gameObject.SetActive(false);
    }
    private void Start()
    {
        
    }
    public void SpawnWeapons()
    {
        levelGUI.SetActive(true);
        Time.timeScale = 0;
        foreach (GameObject item in items)
        {
            gacha_itemList.Add(item);
        }
        if (items.Count <= 4)
        {
            for (int i = 0; i < items.Count; i++)
            {
                GameObject item = items[i];
                GameObject itemSlot = itemSlots[i];
                ItemSlotManager ism = itemSlot.GetComponent<ItemSlotManager>();
                ism.SetData(item);

            }
        }
        else
        {
            for (int i = 0; i < items.Count - 1 || i < itemSlots.Count - 1; i++)
            {
                int randomNum = Mathf.RoundToInt(Random.Range(0, gacha_itemList.Count));
                print(randomNum);
                print(gacha_itemList[randomNum]);
                GameObject item = gacha_itemList[randomNum];
                GameObject itemSlot = itemSlots[i];
                ItemSlotManager ism = itemSlot.GetComponent<ItemSlotManager>();
                ism.SetData(item);
                gacha_itemList.Remove(item);
            }
        }
        gacha_itemList.Clear();
        /*
        for (int i = 0; i < itemSlots.Count-1 || i < items.Count-1; i++)
        {
            print(i);
            int randomNum = Mathf.RoundToInt(Random.Range(0, items.Count - .49f));
            GameObject item = items[randomNum];
            while (gacha_itemList.Contains(item))
            {
                randomNum = Mathf.RoundToInt(Random.Range(0, items.Count - .49f));
                Debug.Log(items[randomNum]);
                item = items[randomNum];
            }
            print(items[randomNum]);
            GameObject itemSlot = itemSlots[i];
            ItemSlotManager ism = itemSlot.GetComponent<ItemSlotManager>();
            ism.SetData(item);
            gacha_itemList.Add(item);
        }
        gacha_itemList.Clear();
        */
    }
    public void SelectWeapon(GameObject weapon)
    {
        levelGUI.SetActive(false);
        Time.timeScale = 1;
        if (weaponPair.ContainsValue(weapon)) UpgradeWeapon(weapon);
        else AddWeapon(weapon);
    }
    public void AddWeapon(GameObject weapon)
    {
        if (weaponPair.Count == weaponSlots.Count) return;
        weaponPair.Add(weaponSlots[weaponPair.Count], weapon);
        UpgradeWeapon(weapon);
        AddWeaponToInventory(weapon);
    }
    
    public void AddWeaponToInventory(GameObject weapon)
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (inventorySlots.Count < 10 && weaponSlots[i].GetComponent<InventorySlotManager>().icon.enabled == false)
            {
                InventorySlotManager ism = weaponSlots[i].GetComponent<InventorySlotManager>();
                print(ism);
                ism.SetIcon(weapon);
                inventorySlots.Add(weapon);
                break;
            }

        }
    }
    public void UpgradeWeapon(GameObject weapon)
    {
        WeaponController wc = weapon.GetComponent<WeaponController>();
        wc.UpdateWeaponLevel();
        if (wc.levelNum == wc.levels.Count)
        {
            items.Remove(weapon);
        }
    }

    IEnumerator wa()
    {
        yield return new WaitForSeconds(2);
        SelectWeapon(items[2]);
        yield return new WaitForSeconds(2);
        SelectWeapon(items[2]);
    }
}
