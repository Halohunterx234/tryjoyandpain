using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum enumfireAI
{
    Horizontal, // left to right
    Horizontal_Backwards,//right to left
    Up, // duh
    Down, // duh
    Quad, // all four
    Auto, // auto aim annd shoots at closest enemy
}

[CreateAssetMenu(menuName = "Create FireAI (reference)")]
public class FireAI : ScriptableObject
{
    [Header("Modes")]
    public enumfireAI fireModes;

    [Header("Reference to its best buddy")]
    public ProjectileAI projModes;

    ItemSuperClassSO current_iSO;
    Transform current_firePoint;
    projAI current_projAIMode;
    GameObject current_player;
    enumfireAI curent_fireAIMode;
    float current_delay;
    public IEnumerator StartFire(float projTotalCount, float projCount, ItemSuperClassSO iSO, Transform firePoint, projAI projAIMode, GameObject player, enumfireAI fireAIMode, float delay)
    {
        //Spawn the projectile and then update its initalized data with
        //the corresponding level weapon's Scriptable Object
        yield return new WaitForSeconds(delay);
        GameObject Projectile = Instantiate(iSO.iProjectileGO, (Vector2)firePoint.position + new Vector2(iSO.iProjectileXOffset, iSO.iProjectileYOffset) * player.transform.localScale.x, Quaternion.identity);
        Projectiles p = Projectile.GetComponent<Projectiles>();
        insert_data(p, iSO);
        //now, give the projectile a different AI depending on which is assigned to it
        switch (fireAIMode)
        {
            case enumfireAI.Horizontal:
                Horizontal(Projectile, player, iSO.iProjectileRot, p);
                break;
            case enumfireAI.Horizontal_Backwards:
                Horizontal_Backwards(Projectile, player, iSO.iProjectileRot, p);
                break;
            case enumfireAI.Up:
                Up(Projectile, player, p);
                break;
            case enumfireAI.Down:
                Down(Projectile, player, p);
                break;
            case enumfireAI.Quad:
                Quad(Projectile, player, p, projCount, projTotalCount, iSO.iProjectileRotDiff);
                break;
            case enumfireAI.Auto:
                Auto();
                break;  
            default:
                yield break;
        }

    }


    //below are the different AI mode for a projectile,
    //if the direction has (alt), it means it has a additional rotation
    //when the player is facing the other way or another direction


    //fire left, and then right (alt)
    public System.Action Horizontal(GameObject proj, GameObject player, float alt_rot, Projectiles p)
    {
        Debug.Log("firing horizontally");
        //make it move in the direction of the player
        projModes.StartAI(proj, player, p.projectileSpeed, new Vector3(-player.transform.localScale.x, 0, 0));
        //Rotate the projectile by (set rotation) if the player turns the other way
        //Set rotation can be changed through the inspector -> projectileRot
        if (player.transform.localScale.x >= 0) RotateProjectile(proj, alt_rot);
        return null;
    }
    //fire the opposite of where the player is looking at
    public System.Action Horizontal_Backwards(GameObject proj, GameObject player, float alt_rot, Projectiles p)
    {
        //shoot in the opposite direction of the player's looking
        projModes.StartAI(proj, player, p.projectileSpeed, new Vector3(player.transform.localScale.x, 0, 0));
        if (player.transform.localScale.x < 0) RotateProjectile(proj, alt_rot);
        return null;
    }
    //fires upwards
    public System.Action Up(GameObject proj, GameObject player, Projectiles p)
    {
        Debug.Log("firing upwards");
        //shoot upwards
        projModes.StartAI(proj, player, p.projectileSpeed, new Vector3(0, Mathf.Abs(Mathf.Sign(player.transform.localScale.x)), 0));
        RotateProjectile(proj, 90);
        return null;
    }

    //fires downwards
    public System.Action Down(GameObject proj, GameObject player, Projectiles p)
    {
        Debug.Log("firing downwards");
        //shoot upwards
        projModes.StartAI(proj, player, p.projectileSpeed, new Vector3(0, -Mathf.Abs(Mathf.Sign(player.transform.localScale.x)), 0));
        RotateProjectile(proj, -90);
        return null;
    }
    public System.Action Quad(GameObject proj, GameObject player, Projectiles p, float count, float totalCount, float angleInterval)
    {
        Vector2 dir = new Vector2(1, 1);
        float theta = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (count >= 1)
        {
            theta += 90 * count;
            theta -= 90;
        }
        else theta -= 45;
        Vector2 newDir = new Vector2(dir.x*Mathf.Cos(theta)-dir.y*Mathf.Sin(theta), dir.x*Mathf.Sin(theta)+dir.y*Mathf.Cos(theta));
        proj.transform.position = (Vector2)proj.transform.position + newDir;
        RotateProjectile(proj, 45 + count * 90);
        projModes.StartAI(proj, player, p.projectileSpeed, newDir.normalized);
        /*
        Vector3 dir = new Vector3(1, 1, 0).normalized;
        dir.Normalize();
        float theta = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
        Debug.Log(theta);
        for (int i = 0; i <= count; i++)
        {
            theta += 90;
        }
        RotateProjectile(proj, 45 + count * 90);
        //Debug.Log(dir.magnitude);
        //Debug.Log(Mathf.Sqrt(Mathf.Pow(dir.x, 2)+ Mathf.Pow(dir.y, 2)));
        Vector2 newDir = new Vector2(dir.magnitude * Mathf.Cos(theta), dir.magnitude * Mathf.Sin(theta));
        Debug.Log(newDir);
        proj.transform.position = (Vector2)proj.transform.position + newDir;
        */
        return null;
    }

    public System.Action Auto()
    {
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
}
