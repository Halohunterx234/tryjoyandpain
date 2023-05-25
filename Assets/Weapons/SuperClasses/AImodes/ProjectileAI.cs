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
    public projAI projMode;

    public void StartAI(GameObject projectile, GameObject player, float pSpeed)
    {
        //Update when neccessary
        switch (projMode)
        {
            case (projAI.Straight):
                Straight(projectile, player, pSpeed);
                break;
            case (projAI.Curved):
                Curved();
                break;
            case (projAI.Homing):
                Homing();
                break;
            default:
                return;
        }
    }
    public System.Action Straight(GameObject proj, GameObject player, float iProjectileSpeed)
    {
        Rigidbody2D projectileRB = proj.GetComponent<Rigidbody2D>();
        projectileRB.velocity = new Vector2(iProjectileSpeed * -player.transform.localScale.x, projectileRB.velocity.y);
        Debug.Log("Bi-Directional Projectile AI -> Fired");
        return null;
    }

    public System.Action Curved()
    {
        Debug.Log("Shoot up and down");
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
