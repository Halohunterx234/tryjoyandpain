using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Create new Modifier SO")]
public class Modifiers : ScriptableObject
{
    //This is a scriptable object that is meant to keep track of upgrades to
    //the stats of the player both permenant and temporary (lasts till game over)
    //perm -> do not clear
    //temp -> clear at start of each game
    //how do the multipliers stack? 

    //function SetPlayerStats()
    //player base stats (at start of game -> default values) * perma-upgrade SO * item-upgrade SO


    //Modifiers (float convert to percentage, i.e 0.1f modifier = +10%)
    public float speedModifier = 0f;
    public float damageModifier = 0f;
    public int healthModifier = 0; //add to both maxhp and currenthp
    public float cdModifier = 0f; 

}
