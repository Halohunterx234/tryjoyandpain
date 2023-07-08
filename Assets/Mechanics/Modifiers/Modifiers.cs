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
    //Speed
    public float speedModifier = 0f;

    //Damage
    public float damageModifier = 0f;

    //Health
    public int maxHealthModifier = 0; //add to both maxhp and currenthp

    //Cooldown Reduction
    public float cdModifier = 0f;

    //Life Regeneration
    public int lifeRegenTrue = 0, lifeRegenAmtModifier = 0; //whether life regen is enabled, how much life to regen per regen
    public float lifeRegenRateModifier = 0f; //the time period for each regen

    //Shield
    public int shieldEnabledTrue = 0, shieldAmtModifier = 0; //whether shield is enabled, how much shield shield shields LOL
    public float shieldRegenRateModifier = 0f; //the time period between each shield regens back to full
}
