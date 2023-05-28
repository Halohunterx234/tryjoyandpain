using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum returnTo
{
    Player,
    Enemy,
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

    [Header("Sprites")]
    public Sprite maxlvlSprite;

    [Header("Projectile Spawn Offsets")]
    [Range(-10f, 10f)]
    public float iProjectileXOffset;
    [Range(-10f, 10f)]
    public float iProjectileYOffset;

    [Header("Projectile Spawn Delay (ie For Stacking Projectiles)")]
    [Range(0f, 2f)]
    public float iProjectileSpawnDelay;

    [Header("Projectile Stats")]
    [Range(0f, 20f)]
    public int iProjectiledamage;

    public float iProjectileknockBack;//if any
    public float CD; public float CDMax;

    public bool canActiviate;
    [Range(0.1f, 10f)]
    public float iProjectileDespawn;
    [Range(1f, 100f)]
    public float iProjectileSpeed;
    [Range(-360f, 360f)]
    public float iProjectileRot;
    [Range(0.1f, 10f)]
    public float iProjectileSize = 1f;

    private int iProjectileMode;

    [Header("Rotation Difference (For Multi-Projectile Types)")]
    public float iProjectileRotDiff;

    [Header("Projectile Count (For Stacking)")]
    public float iProjectileSpawnCount;

    [Header("Projectile AI")]
    public projAI projAIMode;

    [Header("Weapon Firing AI")]
    public enumfireAI fireMode;

    

    [Header("Scriptable Object References")]
    public ProjectileAI projModes;
    public FireAI fireModes;
}
