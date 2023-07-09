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


    //References
    public GameObject player;
    public Player playerController;
    public Modifiers mod; //The itemupgrade_modifier SO to be updated with the corresponding support's buff

    //The current level of the support
    public int level;

    //Max level
    public bool isMax;

    //Description
    public List<string> supportDescriptions;


    // Start is called before the first frame update
    protected void Start()
    {
        level = 0;
        print(level);
        playerController = FindObjectOfType<Player>();
        player = playerController.gameObject;
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    // Levelling Method
    public void LevelUp()
    {
        if (isMax) return;
        level++;
        if (level == Get_MaxLvl())
        {
            isMax = true;
        }
        EnableStats();
    }

    //Initialize level x stats
    public virtual void EnableStats()
    {

    }
    //init
    public virtual void in_it()
    {
        print("turning on");
        this.gameObject.SetActive(true);
        playerController = FindObjectOfType<Player>();
        player = playerController.gameObject;
        level = 0;
    }

    //Reset the effect of the itemMod
    public virtual void Reset()
    {
        
    }

    //Get the max level of the itemMod according to the length of the stats array
    public int Get_MaxLvl()
    {
        return supportDescriptions.Count;
    }
}
