using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningBook : MonoBehaviour
{
    public GameObject book;
    public GameObject openingBook;
    void Deactivate()
    {
        openingBook.SetActive(false);
        book.SetActive(true);
    }
}
