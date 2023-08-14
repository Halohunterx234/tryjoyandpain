using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public RectTransform profileRect;
    //rect of the character icon/ need the size of the character is bigger than reverse arrow icon
    public Sprite[] startingSceneSprites = new Sprite[4];
    //array of the sprites used for the icons
    public Image[] imageUsedToHoldTheSprite= new Image[3];
    //array of the imageUsedToHoldTheSprite
    public GameObject[] screen = new GameObject[5];
    //array of screens 
    public Image[] imageBehindIcon = new Image[4];
    //array of the bookmark that can be coloured
    public Color changedcolor;//the colour the bookmark is changed to
    public Color originalColor;//the original color of the bookmark

    public GameObject credits;//icon for credits
    public GameObject creditsWords;//text used for credit

    public GameObject pageFlipBack;//the paper flipping backward
    public GameObject pageFlipForward;//the paper flipping forward
    public GameObject closingBook;//this is an object with animation of closing the book
    public GameObject book;
    private int previousScreen;//what the previous screen is

    public void Exit()
    {
        StartCoroutine(ExitDelay());//this exit the application
    }
    public IEnumerator ExitDelay()//this cause a delay to the application
    {
        screen[4].SetActive(false);
        book.SetActive(false);
        closingBook.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    public void Instruction()
    {
        
        if(imageUsedToHoldTheSprite[2].sprite == startingSceneSprites[2])
        {
            //if this is pressed with the original sprite icon
            //it will go to the instruction screen and mark the bookmark
            StartCoroutine(ChangeSpriteOfTheIcon(2));
            StartCoroutine(RestartCredit());
            StartCoroutine(GoingToNextScreen(3));
            StartCoroutine(ChangingColor(3));
        }
        else
        {
            //else it will go back to the title page and unmark the bookmark
            StartCoroutine(Interaction(2, 2));
            StartCoroutine(GoingToNextScreen(4));
            StartCoroutine(ChangingColor(4));
        }
    }

    public void Profiles()
    {
        if(imageUsedToHoldTheSprite[1].sprite == startingSceneSprites[1])
        {
            //if this is pressed with the original sprite icon
            //it will go to the profile screen and mark the bookmark
            StartCoroutine(ChangeSpriteOfTheIcon(1));
            StartCoroutine(RestartCredit());
            StartCoroutine(GoingToNextScreen(2));
            StartCoroutine(ChangingColor(2));
        }
        else
        {

            //else it will go back to the title page and unmark the bookmark
            StartCoroutine(Interaction(1,2));
            profileRect.sizeDelta = new Vector2(30, 30);
            StartCoroutine(GoingToNextScreen(4));
            StartCoroutine(ChangingColor(4));
        }
    }

    public void Settings()
    {
        if (imageUsedToHoldTheSprite[0].sprite == startingSceneSprites[0])
        {
            //if this is pressed with the original sprite icon
            //it will go to the setting screen and mark the bookmark
            StartCoroutine(ChangeSpriteOfTheIcon(0));
            StartCoroutine(RestartCredit());
            StartCoroutine(GoingToNextScreen(0));
            StartCoroutine(ChangingColor(0));
        }
        else
        {

            //else it will go back to the title page and unmark the bookmark
            StartCoroutine(Interaction(0, 0));
            StartCoroutine(GoingToNextScreen(4));
            StartCoroutine(ChangingColor(4));
        }
    }

    public void Credits()
    {
        if (!credits.activeInHierarchy)
        {
            //if this is pressed with the original sprite icon
            //it will go to the credit screen and mark the bookmark
            StartCoroutine(ChangingColor(1));
            StartCoroutine(DelayCredit());
            StartCoroutine(GoingToNextScreen(1));

        }
        else
        {
            //else it will go back to the title page and unmark the bookmark     
            StartCoroutine(GoingToNextScreen(4));
            StartCoroutine(ChangingColor(4));
            StartCoroutine(RestartCredit());

        }
    }
    IEnumerator DelayCredit()//there will be a delay before resetting the bookmarks and making the appropriate changes
    {
        yield return new WaitForSeconds(1.5f);
        imageUsedToHoldTheSprite[0].sprite = startingSceneSprites[0];
        imageUsedToHoldTheSprite[1].sprite = startingSceneSprites[1];
        imageUsedToHoldTheSprite[2].sprite = startingSceneSprites[2];
        profileRect.sizeDelta = new Vector2(30, 30);
        credits.SetActive(true);
        creditsWords.SetActive(false);
    }

    IEnumerator ChangeSpriteOfTheIcon(int num)//there will be a delay before resetting the bookmarks and making the appropriate changes
    {
        yield return new WaitForSeconds(1.5f);
        imageUsedToHoldTheSprite[0].sprite = startingSceneSprites[0];
        imageUsedToHoldTheSprite[1].sprite = startingSceneSprites[1];
        imageUsedToHoldTheSprite[2].sprite = startingSceneSprites[2];
        credits.SetActive(false);
        creditsWords.SetActive(true);
        profileRect.sizeDelta = new Vector2(30,30);
        imageUsedToHoldTheSprite[num].sprite = startingSceneSprites[3];
        if (num == 1)
        {
            profileRect.sizeDelta = new Vector2(40, 40);
        }

    }

    IEnumerator GoingToNextScreen(int chosenScreen)//reset the screen then have a delay then make the appropriate changes
    {
        for (int e =0; e < 5; e++)//this make all the screen unactive
        {
            if (screen[e].activeSelf)
            {
                previousScreen = e; 
            }
            screen[e].SetActive(false);
        }
        if(previousScreen> chosenScreen)//determine what animation is used
        {
            pageFlipBack.SetActive(true);
            
        }
        else
        {
            pageFlipForward.SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);
        pageFlipForward.SetActive(false);
        pageFlipBack.SetActive(false);
        screen[chosenScreen].SetActive(true);
    }

    IEnumerator ChangingColor(int num)//have a delay then change the designated bookmark
    {
        yield return new WaitForSeconds(1.5f);
        if (num == 4)
        {
            imageBehindIcon[0].color = originalColor;
            imageBehindIcon[1].color = originalColor;
            imageBehindIcon[2].color = originalColor;
            imageBehindIcon[3].color = originalColor;
        }
        else
        {
            for(int i=0; i < 4; i++)
                {
                if (i == num) imageBehindIcon[i].color = changedcolor;
                else imageBehindIcon[i].color = originalColor;     
                }
        }
        
    }

    IEnumerator Interaction(int imag, int sprit)//have a delay then it make the sprite to the correct sprite
    {
        yield return new WaitForSeconds(1.5f);
        imageUsedToHoldTheSprite[imag].sprite= startingSceneSprites[sprit];        
    }

    IEnumerator RestartCredit()//have a delay then reset the credit icon
    {
        yield return new WaitForSeconds(1.5f);
        credits.SetActive(false);
        creditsWords.SetActive(true);
    }
}
