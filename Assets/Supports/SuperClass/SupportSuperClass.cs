using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSuperClass : MonoBehaviour
{
    //The areas of code, in order to integrate Support Items
    // 1. Make a script for each Support, that is a child of this class,
    // Unlike Weapons, Supports do not follow the same code structure as they function
    // Differently. As such, each Support will be more modular.
    // I don't think Scriptable Objects are required either, so each Support will be 
    // Contained within its' controller script.
    
    // 2. Update the Inventory Manager to include Supports within the gachaList
    // However, make new methods that are specially curated for adding and upgrading Supports Respectively
    // This should be easy enough, as existing methods are made for Weapons only.
    // Thus, there should be little to no conflicts.

    // 3. Code up the Support version of onClick for the Support Items during Levelling-Up

    // 4. Empty gameobject that hosts the SupportController(s) instead of the Weapon gameObject way

    // 5. 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
