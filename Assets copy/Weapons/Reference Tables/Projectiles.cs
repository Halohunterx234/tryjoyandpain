using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    //template stats
    //projectile stats
    protected float projectileSpeed, projectileDamage, projectileKnockback;
    protected float projectileRot;
    protected int projectileMode;
    //References
    GameObject player;
    public Dictionary<int, System.Action> projectileModes = new Dictionary<int, System.Action>();

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        projectileModes.Add(0, BiDirectionalFire);
        projectileModes.Add(1, AutoFire());
        print(projectileModes[0]);
        projectileModes[projectileMode]();
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
        projectileRB.velocity = new Vector2(projectileSpeed * player.transform.localScale.x, projectileRB.velocity.y);
    }
    //Auto Fire Method
    protected System.Action AutoFire()
    {
        return null;
    }
}
