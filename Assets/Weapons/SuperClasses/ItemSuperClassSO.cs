using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public projAI projAIMode;

    [Header("Weapon Firing AI")]
    public enumfireAI fireMode;

    [Header("Scriptable Object References")]
    public ScriptableObject projModes;
    public ScriptableObject fireModes;

}
