using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

[Serializable]
public class HealthValue {
    [HideInInspector] public string Owner;

    [HorizontalGroup("Health")] [LabelText("$Owner"), LabelWidth(90), SuffixLabel("HP", true)]
    public float CurrentHealth;

    [HorizontalGroup("Health"), LabelText("Max"), LabelWidth(30), SuffixLabel("HP", true)]
    public float MaxHealth;

    [CustomValueDrawer("Bar")] public float BarHealth;

    private Color GetHealthBarColor(float value) {
        return Color.Lerp(Color.red, Color.green, Mathf.Pow(value / MaxHealth, 2));
    }

    private float Bar(float value, GUIContent content) {
        Rect rect = EditorGUILayout.GetControlRect();

        ProgressBarConfig config = new ProgressBarConfig();
        config.Height = Convert.ToInt16(rect.height);
        config.ForegroundColor = GetHealthBarColor(CurrentHealth);


        return (float) SirenixEditorFields.ProgressBarField(rect, CurrentHealth, 0, MaxHealth, config);
    }
}