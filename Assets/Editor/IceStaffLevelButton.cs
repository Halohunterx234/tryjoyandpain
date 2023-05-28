using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(IceStaffSuperClass))]
public class IceStaffLevelButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        IceStaffSuperClass eventChannel = (IceStaffSuperClass)target;
        if (GUILayout.Button("Level Up")) { eventChannel.DirectUpdate(); }
    }
}
