using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCanisterController : SupportSuperClass
{
    //Stats
    [Header("Increase Max HP By")]
    [Range(1, 50)]
    public List<int> additionalHPLevels;

    public override void in_it()
    {
        
        //if the level of the current support is > 0 -> weapon is to be upgraded, remove its original buff, and add its upgraded one
        if (level > 1)
        {
            Reset();
        }
        //modify the health of the player here
        this.gameObject.SetActive(true);
        mod.maxHealthModifier += (additionalHPLevels[level - 1]);
        print("hp buff =" + " " + mod.maxHealthModifier);
        player.GetComponent<Player>().SetStats();
    }

    public override void Reset()
    {
        mod.maxHealthModifier -= (additionalHPLevels[level - 1]);
        //undo the movement modifier on the player
        print("hp buff removed");
    }
}
