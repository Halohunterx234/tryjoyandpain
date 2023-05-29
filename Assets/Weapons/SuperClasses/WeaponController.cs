using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : WeaponSuperClass
{
    private void Awake()
    {
        
    }

    private void Start()
    {
        if (this.gameObject.activeSelf && this.gameObject.name != "Pistol") this.gameObject.SetActive(false);
        else
        {
            init();
            InventoryManager im = FindObjectOfType<InventoryManager>();
            print(im);
            im.AddWeapon(this.gameObject);

        }
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
        if (levelNum == 0 )
        {
            init();
        }
        else UpdateLevel();
    }
}
