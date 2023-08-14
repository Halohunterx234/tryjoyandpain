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

    // Spawn at 0 opacity till called
    void Start()
    {
        cd = 0; maxcd = Time.deltaTime / textSpeed;
        iswriting = false;
        dialoguetext = Dialogue.GetComponentsInChildren<Transform>()[5].GetComponent<Text>();
        StartCoroutine(Despawn(0));
    }

    private void Update()
    {
        if (iswriting)
        {
            if (dialoguetext.text.Length == textlength)
            {
                iswriting = false;
                StartCoroutine(Despawn(3));
            }
            else
            {
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

    private IEnumerator changeopacity(Image img, float finalopacity)
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        if (finalopacity == 0)
        {
            while (img.color.a > finalopacity)
            {
                print(img.color.a);
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
