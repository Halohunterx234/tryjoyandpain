using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlurryBootsController : SupportSuperClass
{
    //References
    GameObject player;

    //Stats
    [Header("Increase Movement Speed")]
    [Range(1f, 2f)]
    public List<float> movementSpeedLevels; 

    public override void in_it()
    {
        Reset();
        //modify the speed of the player here
        print("movement buff =" + " " + movementSpeedLevels[level]);
    }

    public override void Reset()
    {
        //undo the movement modifier on the player
        print("movement buff removed");
    }

    public override int Get_MaxLvl()
    {
        return movementSpeedLevels.Count;
    }
}
