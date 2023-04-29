using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpOrbController : MonoBehaviour
{
    CircleCollider2D circleCollider;
    public int xp;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetXP(int xp)
    {
        this.xp = xp;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.GetComponent<Player>())
        {
            XpController xpC = FindObjectOfType<XpController>();
            xpC.AddXP(xp);
            Destroy(this.gameObject);
        }
    }

}
