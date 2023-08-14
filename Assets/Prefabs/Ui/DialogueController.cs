using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameObject Dialogue;

    public Text dialoguetext;

    // Spawn at 0 opacity till called
    void Start()
    {
        dialoguetext = Dialogue.GetComponentsInChildren<Transform>()[5].GetComponent<Text>();
        
    }

    //method to start appearing, increasing opacity as a transition
    public void SpawnDialogue(string newtext)
    {
        TransitionOpacity(100);
    }

    //method to disappear
    private void Despawn()
    {
        TransitionOpacity(0);
    }

    //method to change opacity of images
    private void TransitionOpacity(float finalopacity)
    {
        foreach (GameObject go in Dialogue.GetComponentsInChildren<GameObject>())
        {
            if (go.GetComponent<Text>()) return;
            Image image = go.GetComponent<Image>();
            StartCoroutine(changeopacity(image, finalopacity));
        }
    }

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
        else if (finalopacity == 100)
        {
            while (img.color.a < finalopacity)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + Time.deltaTime);
                yield return w;
            }
        }
    }
}
