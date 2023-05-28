using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PistolSuperClass))]

public class PistolLevelButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PistolSuperClass eventChannel = (PistolSuperClass)target;
        if(GUILayout.Button("Level Up")) { eventChannel.DirectUpdate(); }
    }

}

