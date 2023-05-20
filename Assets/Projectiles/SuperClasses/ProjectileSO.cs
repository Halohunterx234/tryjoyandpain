using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum projAI
{
    BiDirectional,
    Auto
};


[CreateAssetMenu(menuName = "Create new projectile")]
public class ProjectileSO : ScriptableObject
{
    //template stats
    //projectile stats

    //References
    GameObject player, projectile;

    [Header("Projectile AI")]
    public projAI aiMode = new projAI();

    public void init()
    {
        player = FindObjectOfType<Player>().gameObject;
        //projAI[projectileMode]();
        // put this line in the projectile controller script
        //StartCoroutine(DespawnTimer(projectileDespawnTime));
    }

    private System.Action Horizontal()
    {
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
        projectileRB.velocity = new Vector2(projectileSpeed * -player.transform.localScale.x, projectileRB.velocity.y);
        Debug.Log("Bi-Directional Projectile AI -> Fired");
        return null;
    }

    private System.Action Vertical()
    {

        return null;
    }
    private System.Action Auto()
    {
        Debug.Log("Auto method invoked");
        return null;
    }

    private IEnumerator DespawnTimer(float t, GameObject proj)
    {
        yield return new WaitForSeconds(t);
        Destroy(proj);
    }
}