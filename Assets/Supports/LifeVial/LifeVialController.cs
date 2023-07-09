using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeVialController : SupportSuperClass
{
    //Stats
    [Header("Amount of Life to Regen")]
    [Range(1, 10)]
    public List<int> hpRegenAmtLevels;

    [Header("Life Regen Period")]
    [Range(1, 30)]
    public List<float> hpRegenPeriodLevels;
    public void Awake()
    {
        print(player);
    }
    public override void EnableStats()
    {
        //if the level of the current support is > 0 -> weapon is to be upgraded, remove its original buff, and add its upgraded one
        if (level > 1)
        {
            Reset();
        }
        else
        {
            player.AddComponent<LifeRegen>();
            print(player.GetComponent<LifeRegen>());
        }
        //modify the health of the player here
        mod.lifeRegenTrue = 1;
        mod.lifeRegenRateModifier = hpRegenPeriodLevels[level - 1];
        mod.lifeRegenAmtModifier = hpRegenAmtLevels[level - 1];
        print(hpRegenAmtLevels[level - 1]);
        player.GetComponent<Player>().SetStats();
    }

    public override void Reset()
    {
        mod.lifeRegenRateModifier -= hpRegenPeriodLevels[level - 2];
        mod.lifeRegenAmtModifier -= hpRegenAmtLevels[level - 2];
        //undo the movement modifier on the player
    }
}
