using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewStartingScene : MonoBehaviour
{
    public Image instruction;
    public Image profile;
    public RectTransform profileRect;
    public Image settings;
    public Sprite[] startingSceneSprites= new Sprite[4];

    public GameObject credits;
    public GameObject creditsWords;

    public void Instruction()
    {
        if(instruction.sprite == startingSceneSprites[0])
        {
            instruction.sprite = startingSceneSprites[3];
        }
        else
        {
            instruction.sprite = startingSceneSprites[0];
        }
    }

    public void Profiles()
    {
        if(profile.sprite == startingSceneSprites[1])
        {
            profileRect.sizeDelta = new Vector2(15, 15);
            profile.sprite = startingSceneSprites[3];
        }
        else
        {
            profileRect.sizeDelta = new Vector2(10, 10);
            profile.sprite = startingSceneSprites[1];
        }
    }

    public void Settings()
    {
        if (settings.sprite == startingSceneSprites[2])
        {
            settings.sprite = startingSceneSprites[3];
        }
        else
        {
            settings.sprite = startingSceneSprites[2];
        }
    }

    public void Credits()
    {
        if (!credits.activeInHierarchy)
        {
            credits.SetActive(true);
            creditsWords.SetActive(false);
        }
        else
        {
            credits.SetActive(false);
            creditsWords.SetActive(true);

        }
    }


}
