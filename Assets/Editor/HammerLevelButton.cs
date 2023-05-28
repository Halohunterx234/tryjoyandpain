using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HammerSuperClass))]

public class HammerLevelButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        HammerSuperClass eventChannel = (HammerSuperClass)target;
        if(GUILayout.Button("Level Up")) { eventChannel.UpdateLevel(); }
    }

}

