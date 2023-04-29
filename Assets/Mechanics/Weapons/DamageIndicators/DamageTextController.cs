using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextController : MonoBehaviour
{
    Rigidbody2D rb;
    TextMeshPro txt;

    private void Start()
    {
        Destroy(this.gameObject, 1f);
        
    }

    public void ChangeText(string dmg)
    {
        txt = this.gameObject.GetComponent<TextMeshPro>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        txt.text = dmg;
        rb.velocity = new Vector2(1, 1);
        print(dmg);
        //txt.text = dmg;
    }
}
