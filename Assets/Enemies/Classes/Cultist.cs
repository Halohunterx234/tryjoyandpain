using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : MonoBehaviour
{
    //Parent Class for Cultist Enemies
    protected float moveSpeed;
    protected int health;
    protected float damage;
    protected float knockbackResistance;
    public GameObject player;
    Vector3 playerPos;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().gameObject;
        sr = GetComponent<SpriteRenderer>();
    }

    protected void Chase()
    {
        //Move to player's position
        playerPos = player.transform.position;
        Vector3 playerDir = playerPos - transform.position;
        rb.velocity = playerDir.normalized * moveSpeed;
        //Change the rotation if necessary
        if (this.gameObject.transform.position.x > playerPos.x && this.gameObject.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }
        else if (this.gameObject.transform.position.x < playerPos.x && this.gameObject.transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    private void Update()
    {
        Chase();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject projectile = collision.gameObject;
        if (collision.gameObject.GetComponent<Projectiles>())
        {
            health -= projectile.GetComponent<Projectiles>().projectileDamage;
            CheckHealth();
        }
    }

    private void getDamaged(int dmg)
    {
       health -= dmg;
       CheckHealth();
    }

    private void CheckHealth()
    {
        sr.color = Color.red;
        if (health <= 0)
        {
            //Destroy this gameObject for now
            //add in death animations ltr when finished
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(resetColor());
        }

    }
    IEnumerator resetColor()
    {
        yield return new WaitForSeconds(1);
        sr.color = Color.white;
    }
}
