using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum projAI
{
    Straight,
    Curved,
    Homing
}

[CreateAssetMenu(menuName="Create new ProjectileAI (For Reference)")]
public class ProjectileAI : ScriptableObject
{
    [Header("Projectile AI Modes (FOR REFERENCE ONLY)s")]
    public projAI projMode;

    public void StartAI(GameObject projectile, GameObject player, float pSpeed, Vector3 dir, ItemSuperClassSO ism=null)
    {
        //Update when neccessary
        switch (projMode)
        {
            case (projAI.Straight):
                Straight(projectile, pSpeed, dir);
                break;
            case (projAI.Curved):
                Curved(projectile, pSpeed, dir, ism);
                break;
            case (projAI.Homing):
                Homing();
                break;
            default:
                return;
        }
    }

    //make the bullet move in a desired Vector which is given to it by FireAI
    //with current data of the player and the weapon's ISO

    public System.Action Straight(GameObject proj, float iProjectileSpeed, Vector3 dir)
    {
        //get the projectile object & its speed from the ISO
        //the projectile direction will be calculated through fireAI with info
        //from the player
        Rigidbody2D projRB = proj.GetComponent<Rigidbody2D>();
        projRB.velocity = (dir * iProjectileSpeed);
        return null;
    }

    public System.Action Curved(GameObject proj, float iProjectileSpeed, Vector3 dir, ItemSuperClassSO iso)
    {
        Projectiles p = proj.GetComponent<Projectiles>();
        p.isCurving = true;
        Debug.Log(p.isCurving);
        p.curveAngle = iso.projCurveAngle;
        p.curveScale = iso.projCurveScale;
        return null;
    }
    private System.Action Homing()
    {
        Debug.Log("Auto method invoked");
        return null;
    }

    public IEnumerator DespawnTimer(float t, GameObject proj)
    {
        yield return new WaitForSeconds(t);
        Destroy(proj);
    }
}
