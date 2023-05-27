using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSuperClass : WeaponSuperClass
{
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        CDUpdate();
    }

    public override void OnFire()
    {
        base.OnFire();
    }
}
