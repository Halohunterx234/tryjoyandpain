using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Header("References")]
    public ItemSuperClassSO iso;

    [Header("Current AI Mode")]
    public projAI aiMode;

    [Header("Scriptable Object References")]
    public ProjectileAI projModes;
    public FireAI fireModes;


    //template stats
    //projectile stats
    public float projectileSpeed, projectileKnockback;
    public int projectileDamage;
    public float projectileRot, projectileDespawnTime;
    public int projectileMode;

    private float return_after;

    [SerializeField]
    private bool Constant_Rotate;
    [SerializeField]
    [Range(-1000f, 1000f)]
    float Constant_RotationSpeed;

    [Header("Return To __")]
    public bool returning;
    public returnTo return_to;
    //return to specific position after ___ of time
    public float return_to_after;
    [Range(0f, 100f)]
    public float returning_rate;
    [Range(0f, 10f)]
    public float returning_distance;

    private float time;

    //References
    GameObject player;
    public Dictionary<int, System.Action> projectileModes = new Dictionary<int, System.Action>();

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
       // projModes.StartAI(this.gameObject, player, iso.iProjectileSpeed);
        StartCoroutine(projModes.DespawnTimer(iso.iProjectileDespawn, this.gameObject));
        /*
        projectileModes.Add(0, BiDirectionalFire);
        projectileModes.Add(1, AutoFire());
        projectileModes[projectileMode]();
        StartCoroutine(DespawnTimer(projectileDespawnTime));
        */
        time = Time.time;
        
    }
    private void Update()
    {
        if (Constant_Rotate)
        {
            fireModes.RotateProjectile(this.gameObject, -Constant_RotationSpeed * time);
        }
        if (returning) StartCoroutine(return_proj());
        time += Time.deltaTime;
    }

    private bool CheckIfClose(Vector3 projPos, Vector3 targetPos, float dist)
    {
        if (Vector3.Distance(projPos, targetPos) < dist)
        {
            return true;
        }
        else return false;
    }
    private IEnumerator return_proj()
    {
        yield return new WaitForSeconds(return_after);
        switch (return_to)
        {
            case returnTo.Player:
                if (CheckIfClose(this.gameObject.transform.position, player.transform.position, returning_distance))
                {
                    //returning = false;
                    //Destroy(this.gameObject);
                    //yield break;
                }
                StartCoroutine(fireModes.ReturnToPlayer(player, this.gameObject, returning_rate * Time.deltaTime));
                break;
            default:
                yield break;
        }
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
        //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, projectileRot);

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
            if (iso.buff != null)
            {
                collision.gameObject.AddComponent<BuffSuperClass>();
                BuffSuperClass bsc = collision.gameObject.GetComponent<BuffSuperClass>();
                bsc.init_buff(iso.buff);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.isStatic && !collision.gameObject.GetComponent<Player>() && !collision.gameObject.GetComponent<Projectiles>() && !collision.gameObject.GetComponent<XpOrbController>())
        {
            if (iso.buff != null && collision.GetComponent<Entity>())
            {
                collision.gameObject.AddComponent<BuffSuperClass>();
                BuffSuperClass bsc = collision.gameObject.GetComponent<BuffSuperClass>();
                bsc.init_buff(iso.buff);
            }
            Destroy(this.gameObject);
        }
    }
    /*
    IEnumerator DespawnTimer(float i)
    {
        
        yield return new WaitForSeconds(i);
        Destroy(this.gameObject);
    }
    */

}
