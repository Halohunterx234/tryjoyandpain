using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(menuName = "Create new projectile")]
public class ProjectileSO : ScriptableObject
{
    /*
    //template stats
    //projectile stats (to be referred from
    float projectileSpeed;
    [SerializeField]
    float level;

    //References

    GameObject player;
    public GameObject projectile;

    public ItemSuperClassSO itemSC; //weaponsupperclass

    [Header("Projectile AI")]
    public projAI aiMode;

    public void init()
    {
        if (player == null) player = FindObjectOfType<Player>().gameObject;
        //StartCoroutine(DespawnTimer(projectileDespawnTime));
    }

    public void StartAI()
    {
        //Update when neccessary
        switch (aiMode)
        {
            case (projAI.Straight):
                Horizontal();
                break;
            case (projAI.Curved):
                Curved();
                break;
            case (projAI.Auto):
                Auto();
                break;
            default:
                return;
        }
    }
    public System.Action Horizontal()
    {
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
        projectileRB.velocity = new Vector2(projectileSpeed * -player.transform.localScale.x, projectileRB.velocity.y);
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

    private System.Action Quad()
    {
        Debug.Log("Quad method invoked");
        return null;
    }

    private IEnumerator DespawnTimer(float t, GameObject proj)
    {
        yield return new WaitForSeconds(t);
        Destroy(proj);
    }
    */
}