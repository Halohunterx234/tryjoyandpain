using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public BookFlip bk;
    //pause book pages
    public GameObject MainScreen, WeaponsScreen, ItemsScreen;
    //bookmark slots to fill in with data
    public List<GameObject> pItemSlots, pWeaponSlots;
    //references
    InventoryManager im;
    public List<Image> button;
    public Color original;
    public Color changed;

    // Start is called before the first frame update
    void Start()
    {
        // bk.SetActive(false);
        im = this.gameObject.GetComponent<InventoryManager>();
        GoToMain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //pause game and open up the book
    public void BackToTitle()
    {
        UnActivePages();
        Time.timeScale = 1;
        bk.Exit();
        SceneManager.LoadScene("New title scene");
    }

    //unpause game and go back to the level
    public void BackToGame()
    {
        ChangeColor(0, false);
        WeaponsScreen.SetActive(false);
        ItemsScreen.SetActive(false);
        Time.timeScale = 1;
        bk.Exit();
    }

    //unactive pages
    public void UnActivePages()
    {
        MainScreen.SetActive(false);
        WeaponsScreen.SetActive(false);
        ItemsScreen.SetActive(false);
    }
    //swap pages
    public void GoToMain()
    {
        UnActivePages();
        MainScreen.SetActive(true);
        ChangeColor(0,false);
    }

    public void GoToWeapon()
    {
        //if weapon page is already active, go back to main
        if (WeaponsScreen.activeSelf)
        {
            GoToMain();
            return;
        }
        UnActivePages();
        WeaponsScreen.SetActive(true);
        PauseMenuSetData();
        ChangeColor(1);
    }
    public void GoToItems()
    {
        //iif items page is already active, go back to main
        if (ItemsScreen.activeSelf)
        {
            GoToMain();
            return;
        }
        UnActivePages();
        ItemsScreen.SetActive(true);
        ChangeColor(0);

    }
    
    private void ChangeColor(int i, bool changingColor=true)
    {
        button[0].color = original;
        button[1].color = original;
        if (changingColor)
        {
            button[i].color = changed;
        }

    }

    //method to set data of the slots
    public void PauseMenuSetData()
    {
        //set data of all items
        if (ItemsScreen.activeSelf)
        {
            for (int i = 0; i < im.supportPair.Count; i++)
            {
                ItemSlotManager p_ism = pItemSlots[i].GetComponent<ItemSlotManager>();
                p_ism.SetData(im.supportSlots[i].GetComponent<InventorySlotManager>().obj);
            }
        }
        else if (WeaponsScreen.activeSelf)
        {
            for (int i = 0; i < im.weaponPair.Count; i++)
            {
                ItemSlotManager p_ism = pWeaponSlots[i].GetComponent<ItemSlotManager>();
                p_ism.SetData(im.weaponSlots[i].GetComponent<InventorySlotManager>().obj);
            }
        }
    }
}
