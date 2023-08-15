using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningBook : MonoBehaviour
{
    public GameObject book;
    public GameObject openingBook;
    public bool usedInGameScene;
    BookFlip bookflip;
    public void Deactivate()
    {
        if (!usedInGameScene)
        {
            openingBook.SetActive(false);
            book.SetActive(true);
        }
        else
        {
            bookflip = GetComponent<BookFlip>();
            bookflip.deactivate();
        }

    }
}
