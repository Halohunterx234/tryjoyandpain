using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum projAI
{
    Horizontal,
    Vertical,
    Auto
}

[CreateAssetMenu(menuName = "Create new Item")]
public class ItemSuperClassSO : ScriptableObject
{
    //Variables needed for all items
    [Header("Item Level")]
    public int level; 

    [Header("GameObject References")]
    public GameObject iGO;
    public GameObject iProjectileGO;

    [Header("Projectile Spawn Offsets")]
    [Range(-10f, 10f)]
    public float iProjectileXOffset;
    [Range(-10f, 10f)]
    public float iProjectileYOffset;

    [Header("Projectile Stats")]
    public int iProjectiledamage; public float iProjectileknockBack;//if any
    public float CD; public float CDMax;
    public bool canActiviate;
    [Range(1f, 10f)]
    public float iProjectileDespawn;
    [Range(1f, 100f)]
    public float iProjectileSpeed;
    [Range(-360f, 360f)]
    public float iProjectileRot;
    public int iProjectileMode;

    [Header("Projectile AI")]
    public projAI aiMode;

    public void StartAI(GameObject projectile, GameObject player)
    {
        //Update when neccessary
        switch (aiMode)
        {
            case (projAI.Horizontal):
                Horizontal(projectile, player);
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
    public System.Action Horizontal(GameObject proj, GameObject player)
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

    private IEnumerator DespawnTimer(float t, GameObject proj)
    {
        yield return new WaitForSeconds(t);
        Destroy(proj);
    }

}
