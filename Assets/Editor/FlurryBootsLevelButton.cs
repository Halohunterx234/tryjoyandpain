using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlurryBootsController))]
public class FlurryBootsLevelButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        FlurryBootsController eventChannel = (FlurryBootsController)target;
        if (GUILayout.Button("Level Up")) { eventChannel.LevelUp(); }
    }
}
