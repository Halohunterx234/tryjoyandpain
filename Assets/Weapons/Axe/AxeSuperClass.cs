using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSuperClass : WeaponSuperClass
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
