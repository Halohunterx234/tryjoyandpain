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

}
