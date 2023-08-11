using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public BookFlip bk;
    public GameObject MainScreen, WeaponsScreen, ItemsScreen;

    // Start is called before the first frame update
    void Start()
    {
        // bk.SetActive(false);
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
        UnActivePages();
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
    }
    
}
