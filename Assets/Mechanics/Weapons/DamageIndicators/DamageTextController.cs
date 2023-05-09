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

    public void ChangeText(string dmg, Vector3 enemyPos)
    {
        txt = this.gameObject.GetComponent<TextMeshPro>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        txt.text = dmg;
        Vector3 movePos = new Vector2(-Mathf.Sign(FindObjectOfType<Player>().gameObject.transform.position.x - enemyPos.x), -1);
        rb.velocity = movePos;
    }
}
