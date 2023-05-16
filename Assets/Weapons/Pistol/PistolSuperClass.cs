using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PistolSuperClass : WeaponSuperClass
{
    private void Awake()
    {
        init();
    }

    private void Update()
    {
        CDUpdate();
    }

    public override void OnFire()
    {
        base.OnFire();
    }

    /* only if neccessary
    public override void RotateProjectile(GameObject go, float angle)
    {
        base.RotateProjectile(go, angle);
    }
    */
}
