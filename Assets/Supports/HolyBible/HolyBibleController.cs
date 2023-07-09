using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBibleController : SupportSuperClass
{
    //Stats
    [Header("Decrease CD Cooldown By")]
    [Range(1, 2)]
    public List<float> cdReductionLevels;

    public override void EnableStats()
    {
        //if the level of the current support is > 0 -> weapon is to be upgraded, remove its original buff, and add its upgraded one
        if (level > 1)
        {
            Reset();
        }
        //modify the health of the player here
        mod.cdModifier += (cdReductionLevels[level - 1]);
        player.GetComponent<Player>().SetStats();
    }

    public override void Reset()
    {
        mod.cdModifier -= (cdReductionLevels[level - 2]);
        //undo the movement modifier on the player
    }
}
