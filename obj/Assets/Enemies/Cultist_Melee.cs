using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist_Melee : Cultist
{
    private void Awake()
    {
        moveSpeed = 2.5f;
        health = 2;
    }
}