using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AxeSuperClass))]

public class AxeLevelButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AxeSuperClass eventChannel = (AxeSuperClass)target;
        if(GUILayout.Button("Level Up")) { eventChannel.DirectUpdate(); }
    }

}

