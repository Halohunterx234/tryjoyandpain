using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //Lists to keep track of the slots and all unmaxedItems
    //Inventoy slots
    public List<GameObject> weaponSlots;
    public List<GameObject> supportSlots;

    //All unmaxedItems possible to be upgraded/obtained
    public List<GameObject> allItems;
    public List<GameObject> unmaxedItems;

    //A dictionary to keep track of which slot is assigned to obtained unmaxedItems
    public Dictionary<GameObject, GameObject> weaponPair = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, GameObject> supportPair = new Dictionary<GameObject, GameObject>();

    [Header("GUI")]
    public GameObject levelGUI;
    public GameObject inventory;
    public List<GameObject> itemSlots;
    [SerializeField]
    private List<GameObject> gacha_itemList;
    public List<GameObject> inventorySlots;

    //references
    float i;
    GameObject p;
    Modifiers itemMod;
    public GameObject joystick;
    private void Awake()
    {
        levelGUI.gameObject.SetActive(false);
        LoadItems();
        ResetSupportLevel();
    }
    private void Start()
    {
        p = FindObjectOfType<Player>().gameObject;
    }
    //load items from all items into unmaxed items
    private void LoadItems()
    {
        foreach (GameObject item in allItems)
        {
            print(item);
            unmaxedItems.Add(item);
        }
    }
    //reset the levels of all supports
    private void ResetSupportLevel()
    {
        foreach (GameObject item in unmaxedItems)
        {
            item.gameObject.SetActive(true);
            SupportSuperClass ssc = item.GetComponent<SupportSuperClass>();
            if (ssc == null) return;
            ssc.level = 0;
            ssc.player = p;
            ssc.playerController = p.GetComponent<Player>();
            item.gameObject.SetActive(false);
        }
    }
    public void SpawnWeapons()
    {
        //if somehow someone manages to get all upgrades for all unmaxedItems, dont pop-up at all to prevent lock
        if (unmaxedItems.Count == 0) return;

        //freeze and turn on GUI
        levelGUI.SetActive(true);
        Time.timeScale = 0;

        //we have a list of all possible upgradeable/unlockabl unmaxedItems
        foreach (GameObject item in unmaxedItems)
        {
            gacha_itemList.Add(item);
        }
        //if there happens to be 4 or less possible unmaxedItems
        //let the limit be the number of itemslots
        if (unmaxedItems.Count <= 4)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (i + 1 <= unmaxedItems.Count)
                {
                    GameObject item = unmaxedItems[i];
                    GameObject itemSlot = itemSlots[i];
                    ItemSlotManager ism = itemSlot.GetComponent<ItemSlotManager>();
                    ism.SetData(item);
                }
                else
                {
                    GameObject itemSlot = itemSlots[i];
                    ItemSlotManager ism = itemSlot.gameObject.GetComponent<ItemSlotManager>();
                    ism.ClearData();
                }
            }
        }
        //else, go by the number of weapons left and spawn itemMod slots accordingly.
        else
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (i >= unmaxedItems.Count) return;
                int randomNum = Mathf.RoundToInt(Random.Range(0, gacha_itemList.Count));
                GameObject item = gacha_itemList[randomNum];
                print(item);
                print(gacha_itemList);
                GameObject itemSlot = itemSlots[i];
                ItemSlotManager ism = itemSlot.GetComponent<ItemSlotManager>();
                ism.SetData(item);
                gacha_itemList.Remove(item);
            }
        }
        gacha_itemList.Clear();
        /*
        for (int i = 0; i < itemSlots.Count-1 || i < unmaxedItems.Count-1; i++)
        {
            print(i);
            int randomNum = Mathf.RoundToInt(Random.Range(0, unmaxedItems.Count - .49f));
            GameObject itemMod = unmaxedItems[randomNum];
            while (gacha_itemList.Contains(itemMod))
            {
                randomNum = Mathf.RoundToInt(Random.Range(0, unmaxedItems.Count - .49f));
                Debug.Log(unmaxedItems[randomNum]);
                itemMod = unmaxedItems[randomNum];
            }
            print(unmaxedItems[randomNum]);
            GameObject itemSlot = itemSlots[i];
            ItemSlotManager ism = itemSlot.GetComponent<ItemSlotManager>();
            ism.SetData(itemMod);
            gacha_itemList.Add(itemMod);
        }
        gacha_itemList.Clear();
        */
    }

    //Determine if the weapon chosen is to be added for the first time or to be upgraded
    public void SelectWeapon(GameObject weapon)
    {
        levelGUI.SetActive(false);
        Time.timeScale = 1;
        joystick.SetActive(true);
        if (weapon.GetComponent<WeaponController>())
        {
            WeaponController wc = weapon.GetComponent<WeaponController>();
            if (weaponPair.ContainsValue(weapon)) UpgradeItem(wc);
            else AddItem(wc);
        }
        else if (weapon.GetComponent<SupportSuperClass>())
        {
            SupportSuperClass ssc = weapon.GetComponent<SupportSuperClass>();
            if (supportPair.ContainsValue(weapon)) UpgradeItem(ssc); else AddItem(ssc);
        }
    }

    public void AddItem(WeaponController wc)
    {
        if (weaponPair.Count == weaponSlots.Count) return;
        weaponPair.Add(weaponSlots[weaponPair.Count], wc.gameObject);
        UpgradeItem(wc);
        AddItemToInventory(wc);
    }

    public void AddItem(SupportSuperClass ssc)
    {
        if (supportPair.Count == supportSlots.Count) return;
        supportPair.Add(supportSlots[supportPair.Count], ssc.gameObject);
        ssc.in_it();
        print(ssc.level);
        UpgradeItem(ssc);
        AddItemToInventory(ssc);
    }

    //method to add item to inventory
    public void AddItemToInventory(WeaponController wc)
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (inventorySlots.Count < 10 && weaponSlots[i].GetComponent<InventorySlotManager>().icon.enabled == false)
            {
                InventorySlotManager ism = weaponSlots[i].GetComponent<InventorySlotManager>();
                ism.SetIcon(wc.gameObject);
                ism.obj = wc.gameObject;
                inventorySlots.Add(wc.gameObject);
                break;
            }
        }
    }

    //method to add item to inventory
    public void AddItemToInventory(SupportSuperClass ssc)
    {
        for (int i = 0; i < supportSlots.Count; i++)
        {
            if (inventorySlots.Count < 10 && supportSlots[i].GetComponent<InventorySlotManager>().icon.enabled == false)
            {
                InventorySlotManager ism = supportSlots[i].GetComponent<InventorySlotManager>();
                ism.SetIcon(ssc.gameObject);
                ism.obj = ssc.gameObject;
                inventorySlots.Add(ssc.gameObject);
                break;
            }
        }
    }

    //method to upgrade items
    public void UpgradeItem(WeaponController wc)
    {
        wc.UpdateWeaponLevel();
        if (wc.levelNum == wc.levels.Count)
        {
            unmaxedItems.Remove(wc.gameObject);
        }
    }

    //method to upgrade items
    public void UpgradeItem(SupportSuperClass ssc)
    {
        ssc.LevelUp();
        if (ssc.isMax)
        {
            unmaxedItems.Remove(ssc.gameObject);
        }
    }
    
    //method to update the max CDs of the weapons due to holy doll
    public void CDReduce()
    {
        foreach(GameObject item in allItems)
        {
            WeaponController wc = item.GetComponent<WeaponController>();
            if (wc != null)
            {
                wc.CooldownReduce();
            }
        }
    }

}
