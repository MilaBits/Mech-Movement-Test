using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScrapPool : MonoBehaviour {
    [BoxGroup("Scrap")] [HorizontalGroup("Scrap/Values"), LabelWidth(90), SuffixLabel("Scrap", true)]
    public int CurrentScrap;

    [HorizontalGroup("Scrap/Values"), LabelText("Max"), LabelWidth(30), SuffixLabel("Scrap", true)]
    public int MaxScrap;

    [BoxGroup("Scrap")] [CustomValueDrawer("Bar")]
    public int BarScrap;

    [BoxGroup("UI")] [HorizontalGroup("UI/UI"), LabelWidth(30), LabelText("Bar")]
    public RectTransform UIBar;

    [HorizontalGroup("UI/UI"), LabelWidth(35), LabelText("Text")]
    public Text UIText;

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

    private void Update() {
        UIBar.localScale = new Vector3(ExtensionMethods.Remap(CurrentScrap, 0, MaxScrap, 0, 1), 1, 1);
        UIText.text = String.Format("{0} / {1}", CurrentScrap, MaxScrap);
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