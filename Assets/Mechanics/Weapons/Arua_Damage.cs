using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arua_Damage : MonoBehaviour
{//i might be put this into the cultist but tbh i am not too sure   
    [SerializeField]
    protected float fire = 0;
    protected float maxFire = 5;
    protected GameObject player;
    protected float radiusCircle = 5f;
    public LayerMask whoIsEnemy;
    protected int damage = 1;
    public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn) return;
        fire += Time.deltaTime;
        if (fire >= maxFire)
        {
            arua();
            fire = 0f;
        }
    }

    protected void arua()
    {
        Collider2D[] enemyWithin = Physics2D.OverlapCircleAll(player.transform.position, radiusCircle, whoIsEnemy);
        foreach (Collider2D c in enemyWithin)
        {
            print(c.name);
            c.GetComponent<Entity>().GetDamaged(damage);
            print("yes");
        }
    }
}
