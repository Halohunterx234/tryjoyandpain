using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerController : MonoBehaviour
{
    public Modifiers itemMod;
    public AudioSource backgroundMusic;


    GameObject GameOver;
    // Start is called before the first frame update
    void Start()
    {
        GameOver = GameObject.Find("GameOver");
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GameIsOver()
    {
        backgroundMusic.Stop();
        GameOver.SetActive(true);
    }


}
