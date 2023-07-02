using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpOrbController : MonoBehaviour
{
    CircleCollider2D circleCollider;
    public int xp;

    public AudioSource aSource;
    public AudioClip beep;

    bool isPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        aSource = GetComponent<AudioSource>();
        isPickedUp = false;
    }

    public void SetXP(int xp)
    {
        this.xp = xp;
        print(xp);
        transform.localScale = (Vector3.one*0.15f) + Vector3.one*(xp * 0.1f);
        XpController xpC = FindObjectOfType<XpController>();
        xpC.AddXP(xp);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPickedUp) return;
        if (collision != null && collision.gameObject.GetComponent<Player>())
        {
            
            StartCoroutine(beepbeepboopboop());

            XpController xpC = FindObjectOfType<XpController>();
            xpC.AddXP(xp);
           
        }
    }

    IEnumerator beepbeepboopboop()
    {
        isPickedUp = true;
        SpriteRenderer sr;
        sr = GetComponent<SpriteRenderer>();

        sr.color += new Color(0, 0, 0, -100);

        aSource.clip = beep;
        aSource.Play();

        
        yield return new WaitForSeconds(1); 
        Destroy(this.gameObject);
    }

}
