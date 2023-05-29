using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;

[CustomEditor(typeof(WeaponController))]
public class WeaponControllerLevelButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        WeaponController EventChannel = (WeaponController)target;
        if(GUILayout.Button("Level Up")) { EventChannel.DirectUpdate(); }
    }
}
