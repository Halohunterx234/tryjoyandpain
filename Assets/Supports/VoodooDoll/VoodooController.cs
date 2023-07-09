using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoodooController : SupportSuperClass
{
    //adds a shield component to the player, which is in charge of the regen and dmg, etc

    //Stats
    [Header("Amount of Shield to regen")]
    [Range(1, 10)]
    public List<int> shieldRegenAmtLevels;

    [Header("Shield Regen Period")]
    [Range(1, 30)]
    public List<float> shieldRegenPeriodLevels;

    //References
    public UnityEngine.UI.Slider shield_ui; //shield bar to use
    public void Awake()
    {

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
            ShieldController sc = player.AddComponent<ShieldController>();
            sc.slide = shield_ui;
            print(player.GetComponent<ShieldController>());
        }
        //modify the health of the player here
        mod.shieldEnabledTrue = 1;
        mod.shieldRegenRateModifier = shieldRegenPeriodLevels[level - 1];
        mod.shieldRegenAmtModifier = shieldRegenAmtLevels[level - 1];
        player.GetComponent<Player>().SetStats();
    }

    public override void Reset()
    {
        mod.lifeRegenRateModifier -= shieldRegenPeriodLevels[level - 2];
        mod.lifeRegenAmtModifier -= shieldRegenAmtLevels[level - 2];
        //undo the movement modifier on the player
    }
}
