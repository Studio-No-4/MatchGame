using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RelicData))]
public class RelicDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Icon"));
        //var texture = AssetPreview.GetAssetPreview(((RelicData)serializedObject).Icon);
        //GUILayout.Label(texture);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Description"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("OnTurnStart"));
        //EditorGUILayout.EnumPopup(, new());
        serializedObject.ApplyModifiedProperties();
    }
}
