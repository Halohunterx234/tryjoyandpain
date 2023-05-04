using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cultist : Entity
{
    //Parent Class for Cultist Enemies
    public GameObject player;
    Vector3 playerPos;

    public void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        rb = GetComponent<Rigidbody2D>();
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
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject projectile = collision.gameObject;
        if (collision.gameObject.GetComponent<Projectiles>())
        {
            int dmg = projectile.GetComponent<Projectiles>().projectileDamage;
            GetDamaged(dmg);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject projectile = collision.gameObject;
        if (collision.gameObject.GetComponent<Projectiles>())
        {
            int dmg = projectile.GetComponent<Projectiles>().projectileDamage;
            GetDamaged(dmg);
        }
    }
    
public void getDamaged(int dmg)
{
   health -= dmg;
   CheckHealth(dmg);
}

private void CheckHealth(int dmg)
{
    sr.color = Color.red;
    if (health <= 0)
    {
        //Destroy this gameObject for now
        //add in death animations ltr when finished
        Instantiate(xpOrb, transform.position, Quaternion.identity);
        xpOrb.gameObject.GetComponent<XpOrbController>().SetXP(xp);
        GameObject damagetxt = Instantiate(damageText, transform.position + new Vector3(player.transform.localScale.x*4, -2f, 0), Quaternion.identity);
        damagetxt.GetComponent<DamageTextController>().ChangeText(dmg.ToString());
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
*/
}
