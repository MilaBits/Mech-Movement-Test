using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Controls))]
public class ControlsEditor : Editor {
    public override void OnInspectorGUI() {
        EditorGUILayout.LabelField(
            "Inputs in this list should match up with the inputs in Unity's Input Manager.",
            EditorStyles.wordWrappedLabel);
        base.OnInspectorGUI();
    }
}