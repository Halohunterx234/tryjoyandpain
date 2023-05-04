using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    //Big daddy class for everyone that is alive
    protected List<Component> damageClasses; //a list of the classes that can deal damage to this child
    public int hp, maxHp, minHp, xp, collisionDmg;
    protected float moveSpeed, knockbackResistance;
    //Components
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    //Others
    protected bool spawnsDamageTxt, spawnsXpOrb; //is true for all entiites except player
    //References
    public GameObject dmgTxt, xpOrb;

    // Start is called before the first frame update
    void Awake()
    {
        
        minHp = 0;
    }
    private void Start()
    {
        spawnsDamageTxt = this.gameObject.GetComponent<Player>() ? false : true;
        spawnsXpOrb = spawnsDamageTxt; //player shldnt drop xp orbs either
    }
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
            GameObject damagetxt = Instantiate(dmgTxt, transform.position + new Vector3(FindObjectOfType<Player>().gameObject.transform.localScale.x * 4, -2f, 0), Quaternion.identity);
            damagetxt.GetComponent<DamageTextController>().ChangeText(dmg.ToString());
        }
        CheckHealth();
    }

    protected void CheckHealth()
    {
        if (hp <= minHp)
        {
            //drop xp orbs
            if (!spawnsXpOrb)
            {
                Instantiate(xpOrb, transform.position, Quaternion.identity);
                xpOrb.gameObject.GetComponent<XpOrbController>().SetXP(xp);
            }
            OnCheckHealth();
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
    }

}