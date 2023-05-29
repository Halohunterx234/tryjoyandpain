using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public enum enumfireAI
{
    Horizontal, // left to right
    Horizontal_Backwards,//right to left
    Up, // duh
    Down, // duh
    Multi, // all four
    Auto, // auto aim annd shoots at closest enemy
    Random, //random direction cuz why not
}

[CreateAssetMenu(menuName = "Create FireAI (reference)")]
public class FireAI : ScriptableObject
{
    [Header("Modes")]
    public enumfireAI fireModes;

    [Header("Reference to its best buddy")]
    public ProjectileAI projModes;

    [Header("Layers")]
    public LayerMask enemy;

    ItemSuperClassSO current_iSO;
    Transform current_firePoint;
    projAI current_projAIMode;
    GameObject current_player;
    enumfireAI curent_fireAIMode;
    float current_delay;
    GameObject Projectile;
    public IEnumerator StartFire(float levels, float currentlevel, float projTotalCount, float projCount, ItemSuperClassSO iSO, Transform firePoint, projAI projAIMode, GameObject player, enumfireAI fireAIMode, float delay)
    {
        //Spawn the projectile and then update its initalized data with
        //the corresponding level weapon's Scriptable Object
        yield return new WaitForSeconds(delay);
        /*
         * putting audio stuff and play them here
         * put the audioclips reference in the itemsuperclassSO
        AudioClip clip = iSO.fireEffect;
        AudioSource audioSource = player.GetComponent<AudioSource>();
        */
        GameObject Projectile = Instantiate(iSO.iProjectileGO, (Vector2)firePoint.position + new Vector2(iSO.iProjectileXOffset, iSO.iProjectileYOffset) * player.transform.localScale.x, Quaternion.identity); 
        Projectile.GetComponent<Transform>().localScale *= iSO.iProjectileSize;
        Projectiles p = Projectile.GetComponent<Projectiles>();
        insert_data(p, iSO);
        //give it animation if its maxed
        Animator anim = Projectile.GetComponent<Animator>();
        if (anim != null)
        {
            anim.enabled = false;
            if (currentlevel == levels)
            {
                anim.enabled = true;
            }
        }
        if (iSO.maxlvlSprite != null)
        {
            if (currentlevel == levels)
            {
                SpriteRenderer sr = Projectile.GetComponent<SpriteRenderer>();
                sr.sprite = iSO.maxlvlSprite;
            }
        }
        //now, give the projectile a different AI depending on which is assigned to it
        switch (fireAIMode)
        {
            case enumfireAI.Horizontal:
                Horizontal(Projectile, player, p);
                break;
            case enumfireAI.Horizontal_Backwards:
                Horizontal_Backwards(Projectile, player, p);
                break;
            case enumfireAI.Up:
                Up(Projectile, player, p);
                break;
            case enumfireAI.Down:
                Down(Projectile, player, p);
                break;
            case enumfireAI.Multi:
                Multi(Projectile, player, p, projCount, projTotalCount, iSO.iProjectileRotDiff);
                break;
            case enumfireAI.Auto:
                Auto(Projectile, player, enemy, p);
                break;
            case enumfireAI.Random:
                RandomAim(Projectile, player, p);
                break;
            default:
                yield break;
        }
    }


    //below are the different AI mode for a projectile,
    //if the direction has (alt), it means it has a additional rotation
    //when the player is facing the other way or another direction


    //fire left, and then right (alt)
    public System.Action Horizontal(GameObject proj, GameObject player, Projectiles p)
    {
        //make it move in the direction of the player
        projModes.StartAI(proj, player, p.projectileSpeed, Vector3.left * Mathf.Sign(player.transform.localScale.x));
        //Rotate the projectile by (set rotation) if the player turns the other way
        //Set rotation can be changed through the inspector -> projectileRot
        if (player.transform.localScale.x >= 0) RotateProjectile(proj, p.projectileRot);
        return null;
    }
    //fire the opposite of where the player is looking at
    public System.Action Horizontal_Backwards(GameObject proj, GameObject player, Projectiles p)
    {
        //shoot in the opposite direction of the player's looking
        projModes.StartAI(proj, player, p.projectileSpeed, Vector3.left * Mathf.Sign(-player.transform.localScale.x));
        if (player.transform.localScale.x < 0) RotateProjectile(proj, p.projectileRot);
        return null;
    }
    //fires upwards
    public System.Action Up(GameObject proj, GameObject player, Projectiles p)
    {
        //shoot upwards
        projModes.StartAI(proj, player, p.projectileSpeed, Vector3.up);
        RotateProjectile(proj, 90);
        return null;
    }

    //fires downwards
    public System.Action Down(GameObject proj, GameObject player, Projectiles p)
    {
        //shoot upwards
        projModes.StartAI(proj, player, p.projectileSpeed, Vector3.down);
        RotateProjectile(proj, -90);
        return null;
    }
    public System.Action Multi(GameObject proj, GameObject player, Projectiles p, float count, float totalCount, float angleInterval)
    {
        //So we have to first get the angle to spawn the projectile at, relative to its order
        //(i.e the 2nd projectile in a quad (4) fire will need to be rotated more than the 1st one
        //so we get the respective angle
        float angleOffset = (360 / totalCount) * count;

        //then we add any additional angles (lets say you want to rotate the entire thing by another 45 degrees
        float currentAngle = angleOffset + angleInterval;

        //gotta convert it to radians becuz we r using trigo functions later
        float radians = Mathf.Deg2Rad * currentAngle;

        //now we form a direction vector that is in the direction that we want
        Vector2 dir = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        //give it a magntiude of 1 so its legit fr a direction vector
        float distance = 1.0f;

        //now move the projectile to its intended place
        proj.transform.position = (Vector2)player.transform.position + dir * distance;

        //rotate it accordingly to face the correct direction
        RotateProjectile(proj, currentAngle + p.projectileRot);

        //start its relevant AI, given its new rotated direction
        projModes.StartAI(proj, player, p.projectileSpeed, dir.normalized);
        return null;
    }

    public System.Action Auto(GameObject proj, GameObject player, LayerMask enemyLayer, Projectiles p)
    {
        Collider2D RandomEnemy = Physics2D.OverlapCircle(player.transform.position, 20, enemyLayer);
        if (RandomEnemy == null)
        {
            Destroy(proj);
            return null;
        }
        Vector2 autoDir = (RandomEnemy.transform.position - player.transform.position);
        autoDir = autoDir.normalized;
        RotateProjectile(proj, p.projectileRot + (Mathf.Atan2(autoDir.y, autoDir.x) * Mathf.Rad2Deg));
        projModes.StartAI(proj, player, p.projectileSpeed, autoDir);
        return null;
    }

    public System.Action RandomAim(GameObject proj, GameObject player, Projectiles p)
    {
        Vector2 randomDir = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        projModes.StartAI(proj, player, p.projectileSpeed, randomDir.normalized);
        RotateProjectile(proj, p.projectileRot+(Mathf.Atan2(randomDir.y, randomDir.x)*Mathf.Rad2Deg));
        return null;
    }
    public virtual void RotateProjectile(GameObject go, float angle)
    {
        go.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void RotateProjectileAround(GameObject proj, float angle, Vector3 center)
    {
        Vector2 dist = proj.transform.position - center;
        float theta = Mathf.Atan2(dist.y, dist.x) + angle;
        proj.transform.position = new Vector2(dist.magnitude*Mathf.Cos(theta), dist.magnitude*Mathf.Sin(theta));
        Debug.Log(new Vector2(dist.magnitude * Mathf.Cos(theta), dist.magnitude * Mathf.Sin(theta)));
        /*
        //rotate + move the projectile around a given center by a given angle without changing the distance
        //this will be mainly using the player as the center
        Vector2 dist = proj.transform.position - center;
        //convert cartesian coordinates of the dist to polar coordinates
        //then add the additional rotation
        float r = dist.magnitude;
        float theta = Mathf.Atan2(dist.y, dist.x) + angle;
        //convert it back into cartesian
        //and apply it to the position of the projectile
        Vector2 newDist = new Vector2(r*Mathf.Cos(theta), r*Mathf.Sin(theta));
        Debug.Log(newDist); Debug.Log(dist);
        proj.transform.position = newDist;
        proj.transform.rotation.SetLookRotation(newDist.normalized);
        */
    }
    private void insert_data(Projectiles p, ItemSuperClassSO iSO)
    {
        p.projectileDamage = iSO.iProjectiledamage;
        p.projectileKnockback = iSO.iProjectileknockBack;
        p.projectileSpeed = iSO.iProjectileSpeed;
//        p.projectileMode = iSO.iProjectileMode;
        p.projectileDespawnTime = iSO.iProjectileDespawn;
        p.projectileRot = iSO.iProjectileRot;
        p.iso = iSO;
        p.aiMode = iSO.projAIMode;
        
    }

    //Function to return projectile to player
    public IEnumerator ReturnToPlayer(GameObject player, GameObject proj, float rate)
    {
        //Debug.Log("returnin");
        Rigidbody2D projRB = proj.GetComponent<Rigidbody2D>();
        //Debug.Log(projRB.velocity);
        projRB.velocity = projRB.velocity + (Vector2)(rate * (player.transform.position - proj.transform.position));
        yield return null;
    }
}