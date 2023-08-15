using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerController : MonoBehaviour
{
    public Modifiers itemMod;
    public AudioSource backgroundMusic;


    public GameObject GameOver;
    public GameObject restart;
    public GameObject victory;
    public GameObject bookFlipping;
    public GameObject returnGame;
    // Start is called before the first frame update

    public void GameIsOver()
    {
        backgroundMusic.Stop();
        bookFlipping.SetActive(true);
        returnGame.SetActive(false);
        GameOver.SetActive(true);
        restart.SetActive(true);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Time.time >= 30 * 60)
        {
            backgroundMusic.Stop();
            bookFlipping.SetActive(true);
            returnGame.SetActive(false);
            victory.SetActive(true);
            restart.SetActive(true);
        }
    }

}
