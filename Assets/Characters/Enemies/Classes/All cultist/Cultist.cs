using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Cultist : Entity
{
    //Parent Class for Cultist Enemies
    protected GameObject player;
    Vector3 playerPos;
    //public EnemiesSuperClassSO eSO;
    public AiSuperClassSO aiSO;
    public EnemiesSuperClassSO eSO;
    protected Cultist c;
    [SerializeField]
    protected float CD, CDMax;


    public void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c = GetComponent<Cultist>();
        spawnsDamageTxt = true; spawnsXpOrb = spawnsDamageTxt;
        aiSO.insertData(c,eSO);
        xp = eSO.exp;
        particleSize = eSO.particleSize;
    }

    /*public void StartingFire()
    {
        StartCoroutine(aiSO.StartFire());
    }*/

    protected void CDUpdate()
    {
        if (CD >= CDMax)
        {
            CD = 0;
            OnFire();
        }
        else CD += Time.deltaTime;
    }

    public virtual void OnFire()
    {
        //run through each projectile under the levels
        
        /*foreach (ItemSuperClassSO projectile)
        {
            print(projectile);
            for (int i = 0; i <= projectile.iProjectileSpawnCount - 1; i++)
            {
                //pass all information about the projectile to the fireAI method
                StartCoroutine(fireAI.StartFire(levels.Count, levelNum, projectile.iProjectileSpawnCount, i, projectile, player.transform, projectile.projAIMode, player, projectile.fireMode, projectile.iProjectileSpawnDelay));

            }

        }*/

    }
    /*protected void Chase()
    {
        //Move to player's position
        if (player == null) return;
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
    }*/

    private void Update()
    {
        aiSO.StartAi(player, moveSpeed, this.gameObject);
        //Chase();
        CDUpdate();
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
