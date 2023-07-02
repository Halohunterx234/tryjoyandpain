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
    private Color dmgColor = Color.red; //usual color for getting damaged

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

    //When damaged, change color and spawn text
    public void GetDamaged(int dmg)
    {
        GetDamaged(dmg, dmgColor);
    }
    public void GetDamaged(int dmg, Color debuff_color)
    {
        print("color:" + " " + debuff_color);
        sr.color = debuff_color;
        hp -= dmg;
        //spawn dmg
        if (spawnsDamageTxt && dmg != 0)
        {
            GameObject damagetxt = Instantiate(dmgTxt, transform.position, Quaternion.identity);
            damagetxt.GetComponent<DamageTextController>().ChangeText(dmg.ToString(), transform.position);
        }
        CheckHealth();
    }

    public void GetDamaged_ByBuff(int dmg, Color debuff_color)
    {
        GetDamaged(dmg, debuff_color);
        /*
        sr.color = debuff_color;
        hp -= dmg;
        //spawn dmg
        if (spawnsDamageTxt)
        {
            GameObject damagetxt = Instantiate(dmgTxt, transform.position, Quaternion.identity);
            damagetxt.GetComponent<DamageTextController>().ChangeText(dmg.ToString(), transform.position);
        }
        CheckHealth();
        */
    }
    protected void CheckHealth()
    {
        OnCheckHealth();
        if (hp <= minHp)
        {
            //drop xp orbs
            if (spawnsXpOrb)
            {
                GameObject xpOrbGO = Instantiate(xpOrb, transform.position, Quaternion.identity);
                print(xp); 
                xpOrbGO.gameObject.GetComponent<XpOrbController>().SetXP(xp);
            }
            if (this.gameObject.GetComponent<Player>())
            {
                LevelManagerController lmc = FindObjectOfType<LevelManagerController>();
                lmc.GameIsOver();
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
