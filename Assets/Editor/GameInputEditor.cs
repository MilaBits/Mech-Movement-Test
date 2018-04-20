using UnityEditor;

[CustomEditor(typeof(Controls.GameInput))]
public class GameInputEditor : Editor {
    public override void OnInspectorGUI() {
        EditorGUILayout.LabelField("Ps4", serializedObject.FindProperty("Name").stringValue + "_Ps4");
        EditorGUILayout.LabelField("Xbox", serializedObject.FindProperty("Name").stringValue + "_Xbox");
        EditorGUILayout.LabelField("Keyboard", serializedObject.FindProperty("Name").stringValue + "_Keyboard");
        base.OnInspectorGUI();

    }
}