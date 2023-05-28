using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class Player : Entity
{
    //public float moveSpeed = 5f;
    public ArrayList weapons;
    public GameObject pistol;
    public bool isMobile, isPressed;
    public Vector3 movePos;
    public Camera mainCam;

    //references
    Player_HpController hpctrl;
    PlayerHealth ph;

    //For firing weapons
    private void Awake()
    {
        moveSpeed = 5f;
        maxHp = 10;
        hp = maxHp;
        collisionDmg = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        isMobile = false;
        mainCam = Camera.main;
        //weapons.Add(pistol);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hpctrl = GetComponentInChildren<Player_HpController>();
        ph = GetComponentInChildren<PlayerHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Application.isMobilePlatform) Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else
        {
            //print(rb.velocity.x);
            rb.velocity =new Vector2(VirtualJoystick.GetAxis("Horizontal"),
                VirtualJoystick.GetAxis("Vertical"))  * moveSpeed;
            
            if (rb.velocity.x > 0.01 && transform.localScale.x > 0.01)
            {
            transform.localScale = new Vector2(transform.localScale.x *-1,  transform.localScale.y);
            }else if(rb.velocity.x < -0.01 && transform.localScale.x < -0.01)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
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
}
