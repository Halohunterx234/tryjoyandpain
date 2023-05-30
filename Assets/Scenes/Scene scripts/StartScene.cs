using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Instructions()
    {
        SceneManager.LoadScene(2);
    }

    public void Credits()
    {
        SceneManager.LoadScene(3);
    }
}
