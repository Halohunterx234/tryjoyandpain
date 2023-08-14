using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public string loadingScene;
    public string gameScene;
    public bool usedForLoading;
    private void Start()
    {
        if (!usedForLoading) return;
        StartCoroutine(LoadingGame());
    }
    public void StartGame()
    {
        SceneManager.LoadScene(loadingScene);
    }//it load the game

    IEnumerator LoadingGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(gameScene);
    }
}
