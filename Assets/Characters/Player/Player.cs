using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Terresquall;

public class Player : Entity
{
    //public float moveSpeed = 5f;
    public ArrayList weapons;
    public GameObject pistol;
    public bool isPressed;
    public Vector3 movePos;
    public Camera mainCam;
    private bool turnbool;

    //base stats
    private int prevMaxHP; //the previous maxhp before update of stats
    public float cd_red; //cd reduction %

    //references
    Player_HpController hpctrl;
    PlayerHealth ph;
    public GameObject leftDust, rightDust;
    InventoryManager im;
    public Image dmgOverlay;

    //For managing constant damage
    public bool isTagged;
    public int taggedDamaged;
    public List<GameObject> taggedEntities;
    public float tagCD;
    private float tagCDMax;

    //For firing weapons
    private void Awake()
    {
        //health reference
        ph = GetComponentInChildren<PlayerHealth>();
        im = FindObjectOfType<InventoryManager>();
        //clear the temp stats
        ClearStats();
        //set base stats
        SetStats(true);
        //set health to full
        hp = maxHp;
        //no collision damage for player
        collisionDmg = 0;
        taggedDamaged = 0;
        tagCDMax = 1; tagCD = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        //weapons.Add(pistol);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hpctrl = GetComponentInChildren<Player_HpController>();

    }

    // Update is called once per frame
    void Update()
    {
        //check if player is tagged
        if (isTagged)
        {
            //if yes do the total damage of all enemies touching the player every 1 second
            if (tagCD >= tagCDMax)
            {
                tagCD = 0;
                GetDamaged(taggedDamaged);
            }
            else tagCD += Time.deltaTime;
        }
        //if (!UnityEngine.Device.Application.isMobilePlatform) Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //else
        {
            //Check if the player is moving the joystick
            if (VirtualJoystick.GetAxis("Horizontal") != 0 || VirtualJoystick.GetAxis("Vertical") != 0)
            {
                //If yes, assign the RigidBody the corresponding velocity
                rb.velocity = new Vector2(VirtualJoystick.GetAxis("Horizontal"),
                VirtualJoystick.GetAxis("Vertical")) * moveSpeed;
                if (VirtualJoystick.GetAxis("Vertical") > 0.1 && turnbool==false)
                {
                    turnbool = true;
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }else if (VirtualJoystick.GetAxis("Vertical") <-0.1 && turnbool == true)
                {
                    turnbool = false;
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }

                if (rb.velocity.x > 0.01 && transform.localScale.x > 0.01)
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    leftDust.SetActive(true);
                    rightDust.SetActive(false);
                }
                else if (rb.velocity.x < -0.01 && transform.localScale.x < -0.01)
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    leftDust.SetActive(false);
                    rightDust.SetActive(true);
                }
            }
            //If not, set the current velocity to 0
            //This removes friction and gives the player finer movement control
            else
            {
                rb.velocity = new Vector2(0, 0);
                leftDust.SetActive(false);
                rightDust.SetActive(false);
            }

        }
        
    }
    //Movement function
    void Move(float hDir, float vDir)
    {
        if (hDir != 0)
        {
            rb.velocity = new Vector2(hDir * moveSpeed, rb.velocity.y);
            if (transform.localScale.x >= 0 && hDir >= 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
            else if (transform.localScale.x <= 0 && hDir <= 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
        }
        if (vDir != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, vDir * moveSpeed);
        }
        else if (hDir == 0 && vDir == 0)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    protected void FireWeapons()
    {

    }

    //To update the hp bar
    protected override void OnCheckHealth()
    {
        //add a hp bar
        //hpctrl.Set_Values(hp, maxHp, minHp);
        base.OnCheckHealth();
        //if hp is low, make the screen have a bloody red overlay
        //if hp is zero or lower, pause the game and activiate the game over overlay
        if (hp <= 0)
        {
            Time.timeScale = 0;
            return;
        }
        Color dmgColor = Color.red;
        if ((maxHp / hp) >= 3)
        {
            if (hp == 1)
            {
                dmgColor.a = 0.4f;
            }
            else
            {
                dmgColor.a = 0.2f;
            }
        }
        else dmgColor.a = 0;
        dmgOverlay.color = dmgColor;
        ph.SetHealth(hp, maxHp,minHp);
    }

    //public method to update the player's health from external sources
    public void ModifyHp(int newhp)
    {
        hp += newhp;
        CheckHealth();
    }

    //function to set the stats of the player to be as updated as possible
    public void SetStats(bool updateHP=false)
    {
        //player base stats (at start of game -> default values) * (permaMod-upgrade SO + itemMod-upgrade SO + 1)
        //speed
        moveSpeed = 3.5f * (1 + permaMod.speedModifier + itemMod.speedModifier);
        //max health and add the xtra hp to current hp
        if (updateHP)
        {
            prevMaxHP = maxHp;
            maxHp = 10 + permaMod.maxHealthModifier + itemMod.maxHealthModifier;
            hp += maxHp - prevMaxHP;
            CheckHealth();
        }
        //dmg
        //cd - base cd is 0% duh
        cd_red = 0 + permaMod.cdModifier + itemMod.cdModifier;
        im.CDReduce();
        //life regen
        if (itemMod.lifeRegenTrue == 1)
        {
            LifeRegen lr = GetComponent<LifeRegen>();
            lr.maxcd = itemMod.lifeRegenRateModifier;
            lr.lifeAmt = itemMod.lifeRegenAmtModifier;
        }
        if (itemMod.shieldEnabledTrue == 1)
        {
            ShieldController sc = GetComponent<ShieldController>();
            sc.maxCD = itemMod.shieldRegenRateModifier;
            sc.maxShield = itemMod.shieldRegenAmtModifier;
        }
    }

    //function to clear the stats of the itemMod modifiers for the start of each game
    public void ClearStats()
    {
        itemMod.maxHealthModifier = 0;
        itemMod.cdModifier = 0;
        itemMod.speedModifier = 0;
        itemMod.damageModifier = 0;
        itemMod.lifeRegenAmtModifier = 0;
        itemMod.lifeRegenTrue = 0;
        itemMod.lifeRegenRateModifier = 0;
        itemMod.shieldEnabledTrue = 0;
        itemMod.shieldRegenAmtModifier= 0;
        itemMod.shieldRegenRateModifier= 0;
    }
}
