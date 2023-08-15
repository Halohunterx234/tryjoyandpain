using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist_Ranged : Cultist
{
    public GameObject enemyProjectile;

    private void Awake()
    {
        CDMax = 3;
    }
    public override void OnFire()
    {
        if(player = null)
        {
            return;
        }  
        print("firing");
        GameObject enemyProj = Instantiate(enemyProjectile, transform.position + ((player.transform.position) - (transform.position)).normalized*0.1f, Quaternion.identity);
        Rigidbody2D rb = enemyProj.GetComponent<Rigidbody2D>();
        EnemyProjectile ep = enemyProj.GetComponent<EnemyProjectile>();
        rb.velocity = (player.transform.position - transform.position).normalized * ep.speed;
        enemyProj.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(rb.velocity.y, rb.velocity.x)) * Mathf.Rad2Deg);
    }
    
}
