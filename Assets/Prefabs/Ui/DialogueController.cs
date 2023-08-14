using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    //Dialogue object overlay
    public GameObject Dialogue;

    //Dialogue text
    public Text dialoguetext;

    //text options
    public float textSpeed; //speed of text
    //current text
    string text;
    int textlength, textCount; //sum of text's characters and index of text in the list
    float cd, maxcd; //speed of text
    bool iswriting;

    //entrance dialogue text
    public List<string> strings = new List<string>() { "Sister, I am coming for you.","Today..... is the day, that I will avenge my mother.  ", "I still cannot believe the events that have happened the past few weeks. Yet, here I am today. And I will end it all.", "I have a gun and I am going to pew pew all over the place!!!"};
    // Spawn at 0 opacity till called
    void Start()
    {
        //Setting of variables
        cd = 0; maxcd = Time.deltaTime / textSpeed;
        iswriting = false;
        dialoguetext = Dialogue.GetComponentsInChildren<Transform>()[5].GetComponent<Text>();
        //Entrance dialogue
        SpawnDialogue(strings[Random.Range(0, strings.Count-1)]);
    }

    private void Update()
    {
        //if text is to be generated
        if (iswriting)
        {
            if (dialoguetext.text.Length == textlength)
            {
                //text is finished, can start to despawn the box
                iswriting = false;
                StartCoroutine(Despawn(3));
            }
            else
            {
                //generate the text at a readable constant pace
                if (cd >= maxcd)
                {
                    cd = 0;
                    textCount++;
                    dialoguetext.text += text[textCount];
                }
                else cd += Time.deltaTime;
            }
        }
    }

    //method to start appearing, increasing opacity as a transition
    //set everything to default and zero
    public void SpawnDialogue(string newtext)
    {
        TransitionOpacity(1);
        textCount = -1;
        text = newtext;
        textlength = text.Length;
        dialoguetext.text = "";
        iswriting = true;
    }

    //method to disappear
    private IEnumerator Despawn(float delay=0)
    {
        yield return new WaitForSeconds(delay);
        print("despawning");
        TransitionOpacity(0);
    }

    //method to change opacity of images
    private void TransitionOpacity(float finalopacity)
    {
        foreach (Transform tr in Dialogue.GetComponentsInChildren<Transform>())
        {
            GameObject go = tr.gameObject;
            if (go.GetComponent<Text>())
            {
                StartCoroutine(changeopacity(go.GetComponent<Text>(), finalopacity));
            }
            else if (go.GetComponent<Image>())
            {
                Image image = go.GetComponent<Image>();
                print(image);
                StartCoroutine(changeopacity(image, finalopacity));
            }
        }
    }

    //ienumerator to change the opacity over a period of time
    private IEnumerator changeopacity(Image img, float finalopacity)
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        if (finalopacity == 0)
        {
            while (img.color.a > finalopacity)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - Time.deltaTime);
                yield return w;
            }
        }
        else if (finalopacity == 1)
        {
            while (img.color.a < finalopacity)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + Time.deltaTime);
                yield return w;
            }
        }
    }

    //overloaded version for text opacity
    private IEnumerator changeopacity(Text txt, float finalopacity)
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        if (finalopacity == 0)
        {
            while (txt.color.a > finalopacity)
            {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - Time.deltaTime);
                yield return w;
            }
        }
        else if (finalopacity == 1)
        {
            while (txt.color.a < finalopacity)
            {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a + Time.deltaTime);
                yield return w;
            }
        }
    }
}
