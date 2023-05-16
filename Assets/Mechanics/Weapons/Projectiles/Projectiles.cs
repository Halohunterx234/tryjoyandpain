using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    //template stats
    //projectile stats
    public float projectileSpeed, projectileKnockback;
    public int projectileDamage;
    public float projectileRot, projectileDespawnTime;
    public int projectileMode;
    //References
    GameObject player;
    public Dictionary<int, System.Action> projectileModes = new Dictionary<int, System.Action>();

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        projectileModes.Add(0, BiDirectionalFire);
        projectileModes.Add(1, AutoFire());
        projectileModes[projectileMode]();
        StartCoroutine(DespawnTimer(projectileDespawnTime));
    }
    private void Update()
    {
        
    }
    //Pistol Bullet Stats
    //public float pistolBulletSpeed = 7.5f;
    //public float pistolBulletDamage = 1f;
    //public float pistolBulletKnockback = 0.5f;

    //The two aiming methods (currently -> update when needed)
    //Directional Fire Method
    //Algorithm to fire/move the bullet
    //Input Bullet + PlayerPos + BulletSpeed + FireDir
    protected void BiDirectionalFire()
    {
        //Rotate the projectile
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, projectileRot);

        //Move the projectile accordingly
        Rigidbody2D projectileRB = this.gameObject.GetComponent<Rigidbody2D>();
        projectileRB.velocity = new Vector2(projectileSpeed * -player.transform.localScale.x, projectileRB.velocity.y);
    }
    //Auto Fire Method
    protected System.Action AutoFire()
    {
        return null;
    }

    //Collision Events
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.isStatic && !collision.gameObject.GetComponent<Player>() && !collision.gameObject.GetComponent<Projectiles>())
        { 
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.isStatic && !collision.gameObject.GetComponent<Player>() && !collision.gameObject.GetComponent<Projectiles>() && !collision.gameObject.GetComponent<XpOrbController>())
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DespawnTimer(float i)
    {
        
        yield return new WaitForSeconds(i);
        Destroy(this.gameObject);
    }

}
