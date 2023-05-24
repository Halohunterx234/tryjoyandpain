using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum fireAI
{
    Horizontal, // left to right
    Vertical, // down to up
    Quad, // all four

}

[CreateAssetMenu(menuName = "Create FireAI (reference)")]
public class FireAI : ScriptableObject
{
    [Header("Modes")]
    public fireAI fireModes;

    public void StartFire()
    {
        switch (fireModes)
        {
            case fireAI.Horizontal:
                Horizontal();
                break;
            case fireAI.Vertical:
                Vertical();
                break;
            case fireAI.Quad:
                Quad();
                break;
            default:
                return;
        }
    }

    //make a new method of firing for a weapon
    public System.Action Horizontal()
    {

        return null;
    }
    public System.Action Vertical()
    {

        return null;
    }
    public System.Action Quad()
    {

        return null;
    }
}
