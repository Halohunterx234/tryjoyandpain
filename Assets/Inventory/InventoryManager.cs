using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //Lists to keep track of the slots and all items
    //Inventoy slots
    public List<GameObject> weaponSlots;
    public List<GameObject> supportSlots;

    //All items possible to be upgraded/obtained
    public List<GameObject> items;

    //A dictionary to keep track of which slot is assigned to obtained items
    public Dictionary<GameObject, GameObject> weaponPair = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, GameObject> supportPair = new Dictionary<GameObject, GameObject>();

    [Header("GUI")]
    public GameObject levelGUI;
    public GameObject inventory;
    public List<GameObject> itemSlots;
    [SerializeField]
    private List<GameObject> gacha_itemList;
    public List<GameObject> inventorySlots;

    float i;
    GameObject p;
    private void Awake()
    {
        levelGUI.gameObject.SetActive(false);
        ResetSupportLevel();
    }
    private void Start()
    {
        p = FindObjectOfType<Player>().gameObject;
    }

    //reset the levels of all supports
    private void ResetSupportLevel()
    {
        foreach (GameObject item in items)
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
        //if somehow someone manages to get all upgrades for all items, dont pop-up at all to prevent lock
        if (items.Count == 0) return;

        //freeze and turn on GUI
        levelGUI.SetActive(true);
        Time.timeScale = 0;

        //we have a list of all possible upgradeable/unlockabl items
        foreach (GameObject item in items)
        {
            gacha_itemList.Add(item);
        }
        //if there happens to be 4 or less possible items
        //let the limit be the number of itemslots
        if (items.Count <= 4)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (i + 1 <= items.Count)
                {
                    GameObject item = items[i];
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
        //else, go by the number of weapons left and spawn item slots accordingly.
        else
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (i >= items.Count) return;
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

    //Determine if the weapon chosen is to be added for the first time or to be upgraded
    public void SelectWeapon(GameObject weapon)
    {
        levelGUI.SetActive(false);
        Time.timeScale = 1;
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

    public void AddItemToInventory(WeaponController wc)
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (inventorySlots.Count < 10 && weaponSlots[i].GetComponent<InventorySlotManager>().icon.enabled == false)
            {
                InventorySlotManager ism = weaponSlots[i].GetComponent<InventorySlotManager>();
                ism.SetIcon(wc.gameObject);
                inventorySlots.Add(wc.gameObject);
                break;
            }
        }
    }

    public void AddItemToInventory(SupportSuperClass ssc)
    {
        for (int i = 0; i < supportSlots.Count; i++)
        {
            if (inventorySlots.Count < 10 && supportSlots[i].GetComponent<InventorySlotManager>().icon.enabled == false)
            {
                InventorySlotManager ism = supportSlots[i].GetComponent<InventorySlotManager>();
                ism.SetIcon(ssc.gameObject);
                inventorySlots.Add(ssc.gameObject);
                break;
            }
        }
    }
    public void UpgradeItem(WeaponController wc)
    {
        wc.UpdateWeaponLevel();
        if (wc.levelNum == wc.levels.Count)
        {
            items.Remove(wc.gameObject);
        }
    }

    public void UpgradeItem(SupportSuperClass ssc)
    {
        ssc.LevelUp();
        if (ssc.isMax)
        {
            items.Remove(ssc.gameObject);
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
