using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Controller : Weapons
{
    
    public void Awake()
    {
        weaponOffset = new Vector2(1f, 0f);
        weaponCDMax = .5f;
    }

}
