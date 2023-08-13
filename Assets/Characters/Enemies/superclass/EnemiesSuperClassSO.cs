using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Create new Enemies")]
public class EnemiesSuperClassSO : ScriptableObject
{
    [Header("Enemies stat")]
    [Range(0f, 30f)]
    public float moveSpeed;
    [Range(0f, 500f)]
    public int health;
    [Range(0f, 30f)]
    public int collisionDmg;
    [Range(0f, 300f)]
    public int exp;

    [Header("Particle Effect")]
    [Range(0.5f,2f)]
    public float particleSize=1f;

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

    [Header("GameObject References")]
    public GameObject eProjectileGO;

}
