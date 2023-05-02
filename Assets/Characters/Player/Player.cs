using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public ArrayList weapons;
    public GameObject pistol;
    public bool isMobile;

    //For firing weapons
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isMobile = true;
        //weapons.Add(pistol);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMobile) Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 pos = touch.position;
                    if (Vector2.Distance(pos, transform.position) > 0.001f)
                    {
                        var step = moveSpeed * Time.deltaTime;
                        Vector2.MoveTowards(transform.position, pos, step);
                        if (transform.localScale.x >= 0 && (pos.x > transform.position.x))
                        {
                            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                        }
                        else if (transform.localScale.x <= 0 && pos.x < transform.position.x)
                        {
                            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                        }
                    }
                }
            }

        }
        //Every two seconds, we fire all weapons that the player has
        FireWeapons();
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
}
