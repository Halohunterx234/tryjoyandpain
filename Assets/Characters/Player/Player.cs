using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class Player : Entity
{
    //public float moveSpeed = 5f;
    public ArrayList weapons;
    public GameObject pistol;
    public bool isPressed;
    public Vector3 movePos;
    public Camera mainCam;

    //base stats
    private int prevMaxHP; //the previous maxhp before update of stats
    public float cd_red; //cd reduction %

    //references
    Player_HpController hpctrl;
    PlayerHealth ph;
    public GameObject leftDust, rightDust;
    public Modifiers item, perma;
    //For firing weapons
    private void Awake()
    {
        //health reference
        ph = GetComponentInChildren<PlayerHealth>();
        //clear the temp stats
        ClearStats();
        //set base stats
        SetStats(true);
        //set health to full
        hp = maxHp;
        //no collision damage for player
        collisionDmg = 0;

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

        //if (!UnityEngine.Device.Application.isMobilePlatform) Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //else
        {
            //Check if the player is moving the joystick
            if (VirtualJoystick.GetAxis("Horizontal") != 0 || VirtualJoystick.GetAxis("Vertical") != 0)
            {
                //If yes, assign the RigidBody the corresponding velocity
                rb.velocity = new Vector2(VirtualJoystick.GetAxis("Horizontal"),
                VirtualJoystick.GetAxis("Vertical")) * moveSpeed;

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
        /*else if (Input.touchCount > 0)
            {
                if (isPressed)
            {
                movePos = Vector2.zero;
                isPressed = false;
            }
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                Vector2 pos = mainCam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, mainCam.nearClipPlane));
                movePos = pos;
                isPressed = true;
                }
            }
          else if (isPressed)
        {

            if (Vector2.Distance(movePos, transform.position) > 0.001f)
            {
                print(movePos);
                var step = moveSpeed * Time.deltaTime;
                print(step);
                transform.position = Vector2.MoveTowards(transform.position, movePos, step);

                if (transform.localScale.x >= 0 && (movePos.x > transform.position.x))
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                }
                else if (transform.localScale.x <= 0 && movePos.x < transform.position.x)
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                }
            }
            else isPressed = false;
        }
        
        //Every two seconds, we fire all weapons that the player has
        FireWeapons();*/

        
    }

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

    protected override void OnCheckHealth()
    {
        //add a hp bar
        //hpctrl.Set_Values(hp, maxHp, minHp);
        base.OnCheckHealth();
        ph.SetHealth(hp, maxHp,minHp);
    }

    public void publicCheckHealth()
    {
        CheckHealth();
    }

    //function to set the stats of the player to be as updated as possible
    public void SetStats(bool updateHP=false)
    {
        //player base stats (at start of game -> default values) * (perma-upgrade SO + item-upgrade SO + 1)
        //speed
        moveSpeed = 3.5f * (1 + perma.speedModifier + item.speedModifier);
        //max health and add the xtra hp to current hp
        if (updateHP)
        {
            prevMaxHP = maxHp;
            maxHp = 10 + perma.maxHealthModifier + item.maxHealthModifier;
            hp += maxHp - prevMaxHP;
            CheckHealth();
        }
        //dmg
        //cd - base cd is 0% duh
        cd_red = 0 + perma.cdModifier + item.cdModifier;
        //life regen
        if (item.lifeRegenTrue == 1)
        {
            LifeRegen lr = GetComponent<LifeRegen>();
            lr.cd = item.lifeRegenRateModifier;
            lr.lifeAmt = item.lifeRegenAmtModifier;
        }
    }

    //function to clear the stats of the item modifiers for the start of each game
    public void ClearStats()
    {
        item.maxHealthModifier = 0;
        item.cdModifier = 0;
        item.speedModifier = 0;
        item.damageModifier = 0;
        item.lifeRegenAmtModifier = 0;
        item.lifeRegenTrue = 0;
        item.lifeRegenRateModifier = 0;
    }
}
