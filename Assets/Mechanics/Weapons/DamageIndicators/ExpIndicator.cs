using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpIndicator : MonoBehaviour
{
    Rigidbody2D rb;
    TextMeshPro txt;
    float alpha=255;
    public Color expColor;

    private void Start()
    {
        txt = this.gameObject.GetComponent<TextMeshPro>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 1f);
    }
    private void Update()
    {
        alpha -= Time.deltaTime*200;
        expColor.a = alpha;
        txt.color = expColor;
        
    }

    public void ChangeText(string exp)
    {
        txt = this.gameObject.GetComponent<TextMeshPro>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        txt.text = "+" + exp + " " + "XP!";
        Vector3 movePos = new Vector2(0, -0.5f);
        rb.velocity = movePos;
    }
}
