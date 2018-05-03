using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class ScrapPool : MonoBehaviour {
    [HorizontalGroup("Scrap")] public int CurrentScrap;

    [HorizontalGroup("Scrap"), LabelText("Max"), LabelWidth(30)]
    public int MaxScrap;

    [CustomValueDrawer("Bar")] public int BarScrap;

    private Color GetHealthBarColor(float value) {
        return Color.Lerp(Color.red, Color.green, Mathf.Pow(value / MaxScrap, 2));
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class HealthBarAttribute : Attribute {
        public float MaxHealth { get; private set; }

        public HealthBarAttribute(float maxHealth) {
            this.MaxHealth = maxHealth;
        }
    }

#if UNITY_EDITOR

    private int Bar(int value, GUIContent content) {
        Rect rect = EditorGUILayout.GetControlRect();

        ProgressBarConfig config = new ProgressBarConfig();
        config.Height = Convert.ToInt16(rect.height);
        config.ForegroundColor = GetHealthBarColor(CurrentScrap);


        return Convert.ToInt16(
            SirenixEditorFields.ProgressBarField(rect, CurrentScrap, 0, MaxScrap, config));
    }

#endif
}