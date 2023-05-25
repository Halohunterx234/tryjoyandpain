using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum enumfireAI
{
    Horizontal, // left to right
    Vertical, // down to up
    Quad, // all four
    Auto, // auto aim annd shoots at closest enemy
}

[CreateAssetMenu(menuName = "Create FireAI (reference)")]
public class FireAI : ScriptableObject
{
    [Header("Modes")]
    public enumfireAI fireModes;

    public void StartFire(ItemSuperClassSO iSO, Transform firePoint, projAI projAIMode, GameObject player)
    {
        switch (fireModes)
        {
            case enumfireAI.Horizontal:
                Horizontal(iSO, firePoint, projAIMode, player);
                break;
            case enumfireAI.Vertical:
                Vertical();
                break;
            case enumfireAI.Quad:
                Quad();
                break;
            case enumfireAI.Auto:
                Auto();
                break;  
            default:
                return;
        }
    }

    //make a new method of firing for a weapon
    public System.Action Horizontal(ItemSuperClassSO iSO, Transform firePoint, projAI projAIMode, GameObject player)
    {
        GameObject Projectile = Instantiate(iSO.iProjectileGO, (Vector2)firePoint.position + new Vector2(iSO.iProjectileXOffset, iSO.iProjectileYOffset) * player.transform.localScale.x, Quaternion.identity);
        Projectiles p = Projectile.GetComponent<Projectiles>();
        p.projectileDamage = iSO.iProjectiledamage;
        p.projectileKnockback = iSO.iProjectileknockBack;
        p.projectileSpeed = iSO.iProjectileSpeed;
        p.projectileMode = iSO.iProjectileMode;
        p.projectileDespawnTime = iSO.iProjectileDespawn;
        p.projectileRot = iSO.iProjectileRot;
        p.iso = iSO;
        Debug.Log(p.aiMode);
        Debug.Log(iSO.projAIMode);
        p.aiMode = iSO.projAIMode;
        if (player.transform.localScale.x >= 0) RotateProjectile(Projectile, p.projectileRot);
        return null;
    }
    public System.Action Vertical()
    {

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
}
