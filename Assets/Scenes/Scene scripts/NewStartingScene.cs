using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewStartingScene : MonoBehaviour
{
    public RectTransform profileRect;
    public Sprite[] startingSceneSprites = new Sprite[4];
    public Image[] ima= new Image[3];
    public GameObject[] screen = new GameObject[5];
    public Image[] imageBehindIcon = new Image[4];
    public Color color;
    public Color originalColor;

    public GameObject credits;
    public GameObject creditsWords;

    public void Exit()
    {
        Application.Quit();
    }

    public void Instruction()
    {
        if(ima[0].sprite == startingSceneSprites[0])
        {
            ChangeSprite(0);
            RestartCredit();
            CheckingScreen(0);
        }
        else
        {
            Interaction(0, 0);
            CheckingScreen(4);
        }
    }

    public void Profiles()
    {
        if(ima[1].sprite == startingSceneSprites[1])
        {
            ChangeSprite(1);
            RestartCredit();
            CheckingScreen(1);
        }
        else
        {
            Interaction(1,1);
            profileRect.sizeDelta = new Vector2(30, 30);
            CheckingScreen(4);
        }
    }

    public void Settings()
    {
        if (ima[2].sprite == startingSceneSprites[2])
        {
            ChangeSprite(2);
            RestartCredit();
            CheckingScreen(2);
        }
        else
        {
            CheckingScreen(4);
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
            profileRect.sizeDelta = new Vector2(30, 30);
            credits.SetActive(true);
            creditsWords.SetActive(false);
            CheckingScreen(3);

        }
        else
        {
            CheckingScreen(4);
            RestartCredit();

        }
    }

    void ChangeSprite(int num)
    {
        ima[0].sprite = startingSceneSprites[0];
        ima[1].sprite = startingSceneSprites[1];
        ima[2].sprite = startingSceneSprites[2];
        credits.SetActive(false);
        creditsWords.SetActive(true);
        profileRect.sizeDelta = new Vector2(30,30);
        ima[num].sprite = startingSceneSprites[3];
        if (num == 1)
        {
            profileRect.sizeDelta = new Vector2(40, 40);
        }       
    }

    void CheckingScreen(int chosenScreen)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == chosenScreen)
            {
                if (i < 4)
                {
                    imageBehindIcon[i].color = color;
                }
                screen[i].SetActive(true);
            }
            else 
            {
                if (i < 4)
                {
                    imageBehindIcon[i].color = originalColor;
                }
                else if(chosenScreen==4 )
                {
                    imageBehindIcon[0].color = originalColor;
                    imageBehindIcon[1].color = originalColor;
                    imageBehindIcon[2].color = originalColor;
                    imageBehindIcon[3].color = originalColor;
                }
                screen[i].SetActive(false);

            }           
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
