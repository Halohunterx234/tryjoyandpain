using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum enumfireAI
{
    Horizontal, // left to right
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

    public IEnumerator StartFire(ItemSuperClassSO iSO, Transform firePoint, projAI projAIMode, GameObject player, enumfireAI fireAIMode, float delay)
    {
        Debug.Log(iSO.projAIMode);
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
            case enumfireAI.Up:
                Up(Projectile, player, p);
                break;
            case enumfireAI.Down:
                Down(Projectile, player, p);
                break;
            case enumfireAI.Quad:
                Quad();
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
    public System.Action Quad()
    {

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
