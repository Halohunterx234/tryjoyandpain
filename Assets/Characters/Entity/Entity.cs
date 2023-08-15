using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Entity : MonoBehaviour
{
    //Big daddy class for everyone that is alive
    protected List<Component> damageClasses; //a list of the classes that can deal damage to this child
    public int hp, maxHp, minHp, xp, collisionDmg;
    protected float knockbackResistance;
    public float moveSpeed;
    public float particleSize;
    //Components
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    public AudioSource dyingAS;
    //Particles
    public GameObject bloodEffect;
    //Others
    public bool spawnsDamageTxt, spawnsXpOrb; //is true for all entiites except player
    //References
    public GameObject dmgTxt, xpOrb, hpPickUp;
    public Color dmgColor = Color.red; //usual color for getting damaged
    public ScoreManager scoreManager;
    public Modifiers itemMod, permaMod;
    public GameObject iceParticle;
    // Start is called before the first frame update
    void Awake()
    {
        minHp = 0;
        dyingAS = GetComponent <AudioSource>();
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
    //Method to control damage amd shield transfer if present
    public void GetDamaged(int dmg, Color debuff_color)
    {
        sr.color = debuff_color;
        if (this.gameObject.GetComponent<Player>() && itemMod.shieldEnabledTrue == 1)
        {
            //absorb damage if there is shield
            ShieldController sc = this.gameObject.GetComponentInChildren<ShieldController>();
            if (sc.shield > 0)
            {
                if (sc.shield < dmg)
                {
                    dmg -= sc.shield;
                    sc.shield = 0;
                    sc.SetShield();
                }
                else
                {
                    sc.shield -= dmg;
                    dmg = 0;
                    sc.SetShield();
                }
            }
        }
        hp -= dmg;
        //spawn dmg
        if (spawnsDamageTxt && dmg != 0)
        {
            GameObject damagetxt = Instantiate(dmgTxt, transform.position, Quaternion.identity);
            damagetxt.GetComponent<DamageTextController>().ChangeText(dmg.ToString(), transform.position);
        }
        CheckHealth();
    }

    //getdamaged overloaded version that is for buffs only
    public void GetDamaged_ByBuff(int dmg, Color debuff_color)
    {
        GetDamaged(dmg, debuff_color);
    }

    //Deals with the loot and audio portion, basically the death part
    protected void CheckHealth()
    {
        OnCheckHealth();
        if (hp <= minHp)
        {
            GameObject dying_vfx = Instantiate(bloodEffect, transform.position, Quaternion.identity);
            dying_vfx.transform.localScale *= particleSize;
            Destroy(dying_vfx, 0.7f);
            //drop xp orbs
            if (spawnsXpOrb)
            {
                GameObject xpOrbGO = Instantiate(xpOrb, transform.position, Quaternion.identity);
                xpOrbGO.gameObject.GetComponent<XpOrbController>().SetXP(xp);
                int determineHPDrop = Random.Range(0, 100);
                if (determineHPDrop <= 5)
                {
                    Instantiate(hpPickUp, transform.position, Quaternion.identity);
                }
                AudioSource dyingVFXAS = dying_vfx.AddComponent<AudioSource>();
                dyingVFXAS.clip = dyingAS.clip;
                dyingVFXAS.playOnAwake = false;
                dyingVFXAS.Play();
            }
            if (this.gameObject.GetComponent<Player>())
            {
                scoreManager.Calc_TimeScore();
                LevelManagerController lmc = FindObjectOfType<LevelManagerController>();
                lmc.GameIsOver();
            }
            scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.Calc_KillCount(1);
            if (this.gameObject.GetComponent<Player>())
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
        else
        {
            StartCoroutine(ResetColor());
        }
    }

    //left empty for inheritance
    protected virtual void OnCheckHealth()
    {

    } 

    //Reset color to default after getting damaged
    public IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(1);
        sr.color = Color.white;
    }

    //Collision Code for both collision and trigger
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.gameObject.GetComponent<Player>() && collision.gameObject.GetComponent<Entity>())
        {
            if (!collision.gameObject.GetComponent<BoxCollider2D>() && !collision.gameObject.GetComponent<CircleCollider2D>())
            {
                this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            }
            else this.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
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

    //Mechanic to deal damage to player if remaining in enemy contact for extended periods of time
    //damage is also stacked with the number of enemies touching the player
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if enemy is still touching the player
        if (collision.GetComponent<Entity>() && this.gameObject.GetComponent<Player>())
        {
            Player player = this.gameObject.GetComponent<Player>();
            //if this is the first time player is tagged by a enemy, switch the state accordingly
            if (!player.isTagged) { player.isTagged = true; }
            //if this entity is newly tagged to the player, add it and stack its collision damage
            if (!player.taggedEntities.Contains(collision.gameObject))
            {
                player.taggedEntities.Add(collision.gameObject);
                player.taggedDamaged += collision.gameObject.GetComponent<Entity>().collisionDmg;
                return;
            }
        }
    }

    //stop player tag when no entities
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() && this.gameObject.GetComponent<Player>())
        {
            Player player = this.gameObject.GetComponent<Player>();
            player.taggedEntities.Remove(collision.gameObject);
            player.taggedDamaged -= collision.gameObject.GetComponent<Entity>().collisionDmg;
            if (player.taggedEntities.Count == 0)
            {
                player.isTagged = false;
                player.tagCD = 0;
            }
        }
    }
}
