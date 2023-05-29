using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(XpController))]

public class XpLevelButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        XpController eventChannel = (XpController)target;
        if(GUILayout.Button("Level Up")) { eventChannel.AddXP(10); }
    }

}

