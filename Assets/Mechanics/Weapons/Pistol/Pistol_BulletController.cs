using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_BulletController : Projectiles
{
    private void Awake()
    {
        projectileSpeed = 20f;
        projectileRot = 90f;
        projectileDamage = 1;
        projectileKnockback = 0.25f;
        projectileDespawnTime = 2.5f;
        projectileMode = 0; //Bi-directional Firing
    }
}
