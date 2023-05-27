using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AxeSuperClass))]
public class ItemLevelButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AxeSuperClass eventChannel = (AxeSuperClass)target;
        if(GUILayout.Button("Raise Event")) { eventChannel.UpdateLevel(); }
    }
}

