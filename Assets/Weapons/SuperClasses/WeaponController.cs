using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : WeaponSuperClass
{
    private void Awake()
    {
        if (this.gameObject.activeSelf && this.gameObject.name != "Pistol") this.gameObject.SetActive(false);
        else init();
    }

    private void Update()
    {
        CDUpdate();
    }

    public override void OnFire()
    {
        base.OnFire();
    }

    public void UpdateWeaponLevel()
    {
        Debug.Log(levelNum);
        if (levelNum == 0 )
        {
            init();
        }
        else UpdateLevel();
    }
}
