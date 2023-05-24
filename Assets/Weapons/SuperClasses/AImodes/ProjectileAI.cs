using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum projAI
{
    Horizontal,
    Vertical,
    Auto
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
            case (projAI.Horizontal):
                Horizontal(projectile, player, pSpeed);
                break;
            case (projAI.Vertical):
                Vertical();
                break;
            case (projAI.Auto):
                Auto();
                break;
            default:
                return;
        }
    }
    public System.Action Horizontal(GameObject proj, GameObject player, float iProjectileSpeed)
    {
        Rigidbody2D projectileRB = proj.GetComponent<Rigidbody2D>();
        projectileRB.velocity = new Vector2(iProjectileSpeed * -player.transform.localScale.x, projectileRB.velocity.y);
        Debug.Log("Bi-Directional Projectile AI -> Fired");
        return null;
    }

    public System.Action Vertical()
    {
        Debug.Log("Shoot up and down");
        return null;
    }
    private System.Action Auto()
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
