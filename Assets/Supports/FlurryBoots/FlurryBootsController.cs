using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlurryBootsController : SupportSuperClass
{

    //Stats
    [Header("Increase Movement Speed")]
    [Range(1f, 2f)]
    public List<float> movementSpeedLevels;

    public override void in_it()
    {
        //if the level of the current support is > 0 -> weapon is to be upgraded, remove its original buff, and add its upgraded one
        if (level > 1)
        {
            Reset();
        }
        //modify the speed of the player here
        print("movement buff =" + " " + movementSpeedLevels[level-1]);
        this.gameObject.SetActive(true);
        mod.speedModifier += (movementSpeedLevels[level - 1] - 1);
        playerController.SetStats();
    }

    public override void Reset()
    {
        mod.speedModifier -= (movementSpeedLevels[level-1]-1);
        //undo the movement modifier on the player
        print("movement buff removed");
    }
}
