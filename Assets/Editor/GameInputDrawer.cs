using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Controls.GameInput))]
public class GameInputDrawer : PropertyDrawer {
    private string name;
    private bool cache;

    private float propertyHeight = 0;
    private float rowHeight = 20;
    private float padding = 5;
    private int indent;
    private Rect contentPosition;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = -5;

        contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(name));

        SerializedProperty Name = property.FindPropertyRelative("Name");
        SerializedProperty Ps4 = property.FindPropertyRelative("Ps4");
        SerializedProperty Xbox = property.FindPropertyRelative("Xbox");
        SerializedProperty Keyboard = property.FindPropertyRelative("Keyboard");

        EditorGUI.BeginProperty(contentPosition, label, Name);
        {
            EditorGUI.BeginChangeCheck();
            string newVal = EditorGUI.TextField(contentPosition, new GUIContent("Name"), Name.stringValue);
            if (EditorGUI.EndChangeCheck())
                Name.stringValue = newVal;
        }
        EditorGUI.EndProperty();
        contentPosition.y += 20;

        EditorGUI.indentLevel = EditorGUI.indentLevel + 1;
        EditorGUI.BeginProperty(contentPosition, label, Ps4);
        {
            EditorGUI.BeginChangeCheck();
            string newVal = EditorGUI.TextField(contentPosition, new GUIContent("Ps4"), Ps4.stringValue);
            if (EditorGUI.EndChangeCheck())
                Ps4.stringValue = newVal;
        }
        EditorGUI.EndProperty();
        contentPosition.y += 20;
        EditorGUI.BeginProperty(contentPosition, label, Xbox);
        {
            EditorGUI.BeginChangeCheck();
            string newVal = EditorGUI.TextField(contentPosition, new GUIContent("Xbox"), Xbox.stringValue);
            if (EditorGUI.EndChangeCheck())
                Xbox.stringValue = newVal;
        }
        EditorGUI.EndProperty();
        contentPosition.y += 20;
        EditorGUI.BeginProperty(contentPosition, label, Keyboard);
        {
            EditorGUI.BeginChangeCheck();
            string newVal = EditorGUI.TextField(contentPosition, new GUIContent("Keyboard"), Keyboard.stringValue);
            if (EditorGUI.EndChangeCheck())
                Keyboard.stringValue = newVal;
        }
        EditorGUI.EndProperty();


        EditorGUI.indentLevel = 0;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return 20f;
    }

    //    public override void OnInspectorGUI() {
//        EditorGUILayout.LabelField("Ps4", serializedObject.FindProperty("Name").stringValue + "_Ps4");
//        EditorGUILayout.LabelField("Xbox", serializedObject.FindProperty("Name").stringValue + "_Xbox");
//        EditorGUILayout.LabelField("Keyboard", serializedObject.FindProperty("Name").stringValue + "_Keyboard");
//        base.OnInspectorGUI();
//    }
}