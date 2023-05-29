using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    //Big daddy class for everyone that is alive
    protected List<Component> damageClasses; //a list of the classes that can deal damage to this child
    public int hp, maxHp, minHp, xp, collisionDmg;
    protected float knockbackResistance;
    public float moveSpeed;
    //Components
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    //Others
    public bool spawnsDamageTxt, spawnsXpOrb; //is true for all entiites except player
    //References
    public GameObject dmgTxt, xpOrb;

    // Start is called before the first frame update
    void Awake()
    {
        minHp = 0;
    }
    //protected void Start()
    //{
        //spawnsDamageTxt = true;
        //spawnsXpOrb = spawnsDamageTxt; //player shldnt drop xp orbs either
    //}
    // Update is called once per frame
    void Update()
    {

    }

    public void GetDamaged(int dmg)
    {
        sr.color = Color.red;
        hp -= dmg;
        //spawn dmg
        if (spawnsDamageTxt)
        {
            GameObject damagetxt = Instantiate(dmgTxt, transform.position, Quaternion.identity);
            print(transform.position); print(damagetxt.transform.position);
            damagetxt.GetComponent<DamageTextController>().ChangeText(dmg.ToString(), transform.position);
        }
        CheckHealth();
    }

    protected void CheckHealth()
    {
        OnCheckHealth();
        if (hp <= minHp)
        {
            //drop xp orbs
            if (spawnsXpOrb)
            {
                Instantiate(xpOrb, transform.position, Quaternion.identity);
                xpOrb.gameObject.GetComponent<XpOrbController>().SetXP(xp);
            }
            
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(ResetColor());
        }
    }

    protected virtual void OnCheckHealth()
    {

    } 

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(1);
        sr.color = Color.white;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Projectiles>() && !this.gameObject.GetComponent<Player>())
        {
            GetDamaged(collision.gameObject.GetComponent<Projectiles>().projectileDamage);
        }
        else if (collision.GetComponent<Entity>() && this.gameObject.GetComponent<Player>())
        {
            GetDamaged(collision.gameObject.GetComponent<Entity>().collisionDmg);
        }
        else if(collision.GetComponent<EnemyProjectile>() && this.gameObject.GetComponent<Player>())
        {
            GetDamaged(collision.gameObject.GetComponent<EnemyProjectile>().dmg);
        }
    }

}
