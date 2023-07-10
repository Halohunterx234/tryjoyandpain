using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewStartingScene : MonoBehaviour
{
    public RectTransform profileRect;
    public Sprite[] startingSceneSprites = new Sprite[4];
    public Image[] ima= new Image[3];

    public GameObject credits;
    public GameObject creditsWords;

    public void Instruction()
    {
        if(ima[0].sprite == startingSceneSprites[0])
        {
            Restart(0);
            RestartCredit();
        }
        else
        {
            Interaction(0, 0);
        }
    }

    public void Profiles()
    {
        if(ima[1].sprite == startingSceneSprites[1])
        {
            Restart(1);
            RestartCredit();
        }
        else
        {
            Interaction(1,1);
            profileRect.sizeDelta = new Vector2(10, 10);
        }
    }

    public void Settings()
    {
        if (ima[2].sprite == startingSceneSprites[2])
        {
            Restart(2);
            RestartCredit();
        }
        else
        {
            Interaction(2, 2);
        }
    }

    public void Credits()
    {
        if (!credits.activeInHierarchy)
        {
            ima[0].sprite = startingSceneSprites[0];
            ima[1].sprite = startingSceneSprites[1];
            ima[2].sprite = startingSceneSprites[2];
            profileRect.sizeDelta = new Vector2(10, 10);
            credits.SetActive(true);
            creditsWords.SetActive(false);
        }
        else
        {
            RestartCredit();

        }
    }

    void Restart(int num)
    {
        ima[0].sprite = startingSceneSprites[0];
        ima[1].sprite = startingSceneSprites[1];
        ima[2].sprite = startingSceneSprites[2];
        credits.SetActive(false);
        creditsWords.SetActive(true);
        profileRect.sizeDelta = new Vector2(10,10);
        ima[num].sprite = startingSceneSprites[3];
        if (num == 1)
        {
            profileRect.sizeDelta = new Vector2(15, 15);
        }
    }

    void Interaction(int imag, int sprit)
    {
        ima[imag].sprite= startingSceneSprites[sprit];        
    }
    void RestartCredit()
    {
        credits.SetActive(false);
        creditsWords.SetActive(true);
    }


}
