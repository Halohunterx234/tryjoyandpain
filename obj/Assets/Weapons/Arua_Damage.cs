using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arua_Damage : MonoBehaviour
{
    [SerializeField]
    protected float fire=0;
    protected float maxFire=2;
    protected GameObject player;
    protected float radiusCircle=20f;
    protected LayerMask whoIsEnemy;
    protected int damage=1;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        whoIsEnemy = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        fire += Time.deltaTime;
        if (fire >= maxFire)
        {
            arua();
            fire = 0f;
        }
    }

    protected void arua()
    {        
        Collider2D[] enemyWithin = Physics2D.OverlapCircleAll(player.transform.position , radiusCircle, whoIsEnemy);
        foreach(Collider2D c in enemyWithin)
        {
            print("yes");
            c.GetComponent<Cultist>().getDamaged(damage);
        }
    }
}
