using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlowpipeSuperClass))]

public class BlowpipeLevelButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BlowpipeSuperClass eventChannel = (BlowpipeSuperClass)target;
        if(GUILayout.Button("Level Up")) { eventChannel.DirectUpdate(); }
    }

}

