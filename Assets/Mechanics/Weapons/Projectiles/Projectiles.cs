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
    public int projectilePenMax; private int projectilePenCount;

    [SerializeField]
    private bool Constant_Rotate;
    [SerializeField]
    [Range(-1000f, 1000f)]
    float Constant_RotationSpeed;

    [Header("Return To __")]
    private float return_after;
    public bool returning;
    public returnTo return_to;
    //return to specific position after ___ of time
    public float return_to_after;
    [Range(0f, 100f)]
    public float returning_rate;
    [Range(0f, 10f)]
    public float returning_distance;

    [Header("Curve")]
    public bool isCurving;
    public double curveAngle;
    public float curveScale;
    public Vector3 curveDir;
    public Vector3 originalDir;

    private float time;

    //References
    GameObject player;
    Rigidbody2D rb;
    public Dictionary<int, System.Action> projectileModes = new Dictionary<int, System.Action>();

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(projModes.DespawnTimer(iso.iProjectileDespawn, this.gameObject));
        projectilePenCount = 0;
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
        if (isCurving) Curve();
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

    private void Curve()
    {
        if (curveDir.normalized != originalDir.normalized)
        {
            float newangle = Mathf.LerpAngle(Mathf.Atan2(originalDir.normalized.y, originalDir.normalized.x) * Mathf.Rad2Deg, Mathf.Atan2(curveDir.normalized.y, curveDir.normalized.x) * Mathf.Rad2Deg, time * curveScale);
            float newangleradians = newangle * Mathf.Deg2Rad;
            Vector3 newDir = new Vector2(Mathf.Cos(newangleradians), Mathf.Sin(newangleradians));
            rb.velocity = newDir * projectileSpeed;
        }
        else isCurving = false;
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
        if (collision.gameObject.GetComponent<EnemyProjectile>()) return;   
        if (!collision.gameObject.isStatic && !collision.gameObject.GetComponent<Player>() && !collision.gameObject.GetComponent<Projectiles>())
        { 
            if (iso.buff != null && collision.gameObject.GetComponent<Entity>())
            {
                bool isPresent = false;
                BuffSuperClass[] bscs = collision.gameObject.GetComponents<BuffSuperClass>();
                foreach (BuffSuperClass bsc in bscs)
                {
                    if (bsc != null && bsc.currentBuff == iso.buff)
                    {
                        bsc.ResetCD();
                        isPresent = true;
                        break;
                    }
                }
                if (!isPresent)
                {
                    BuffSuperClass newBSC = collision.gameObject.AddComponent<BuffSuperClass>();
                    newBSC.init_buff(iso.buff);
                }
            }
            projectilePenCount++;
            if (projectilePenCount >= projectilePenMax)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyProjectile>()) return;
        if (!collision.gameObject.isStatic && !collision.gameObject.GetComponent<Player>() && !collision.gameObject.GetComponent<Projectiles>() && !collision.gameObject.CompareTag("Pickups"))
        {
            if (iso.buff != null && collision.gameObject.GetComponent<Entity>())
            {
                bool isPresent = false;
                BuffSuperClass[] bscs = collision.gameObject.GetComponents<BuffSuperClass>();
                foreach (BuffSuperClass bsc in bscs)
                {
                    if (bsc != null && bsc.currentBuff == iso.buff)
                    {
                        bsc.ResetCD();
                        isPresent = true;
                        break;
                    }
                }
                if (!isPresent)
                {
                    BuffSuperClass newBSC = collision.gameObject.AddComponent<BuffSuperClass>();
                    newBSC.init_buff(iso.buff);
                }
            }
            projectilePenCount++;
            if (projectilePenCount >= projectilePenMax)
            {
                Destroy(this.gameObject);
            }
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
