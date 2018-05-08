using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Health : MonoBehaviour {
    [HorizontalGroup("Health/module"), LabelText("Module"), LabelWidth(60)]
    public bool isModuleHealth;

    [HorizontalGroup("Health/module"), LabelWidth(90), ShowIf("isModuleHealth")]
    public Health ParentHealth;

    [BoxGroup("Health")] public HealthValue health;

    [HideIf("isModuleHealth"), HorizontalGroup("Health/Base"), LabelWidth(90), LabelText("Mech Health"),
     SuffixLabel("HP", true)]
    public float BaseCurrentHealth;

    [HideIf("isModuleHealth"), HorizontalGroup("Health/Base"), LabelWidth(30), LabelText("Max"),
     SuffixLabel("HP", true)]
    public float BaseMaxHealth;

    [BoxGroup("Health/UI")]
    [HideIf("isModuleHealth"), HorizontalGroup("Health/UI/UI"), LabelWidth(30), LabelText("Bar")]
    public RectTransform UIBar;

    [HideIf("isModuleHealth"), HorizontalGroup("Health/UI/UI"), LabelWidth(35), LabelText("Text")]
    public Text UIText;

    [ShowIf("isModuleHealth"), BoxGroup("Health/UI"), LabelWidth(60), LabelText("HUD Parts"),
     Tooltip("Put the UI gamaeobjects here that should change color according to health left.")]
    public Image[] UIMech;

    [BoxGroup("Health/UI"), LabelText("Health Colors")]
    public Gradient gradient;

    [BoxGroup("Health"), HideIf("isModuleHealth")]
    public List<HealthValue> Modules;

    [FoldoutGroup("Damage")] public bool Critical;
    [FoldoutGroup("Damage")] public float CriticalMultiplier = 1.5f;
    [Space] [FoldoutGroup("Damage")] public Material DamagedMaterial;

    [FoldoutGroup("Damage")] [SerializeField]
    private GameObject damagePopup;

    [FoldoutGroup("Damage")] [SerializeField]
    private Vector3 popupOffset;


    // Use this for initialization
    void Start() {
        if (isModuleHealth) {
            health.CurrentHealth = health.MaxHealth;
            health.Owner = gameObject.name;
        }
        else {
            health.Owner = "Combined";
        }
    }

    private void Update() {
        if (ParentHealth) ParentHealth.CalculateHealth();

        if (!isModuleHealth) {
            if (UIBar)
                UIBar.localScale = new Vector3(ExtensionMethods.Remap(health.CurrentHealth, 0, health.MaxHealth, 0, 1), 1, 1);
            if (UIText) UIText.text = String.Format("{0} / {1}", health.CurrentHealth, health.MaxHealth);
            return;
        }

        foreach (var image in UIMech) {
            image.color = GetHealthBarColor(health.CurrentHealth);
        }
    }

    public void UpdateHealthList() {
        Modules.Clear();
        Health[] healthChildren = transform.GetComponentsInChildren<Health>();
        for (int i = 0; i < healthChildren.Length; i++) {
            if (i == 0) continue;
            HealthValue value = healthChildren[i].health;
            value.Owner = healthChildren[i].name;
            Modules.Add(healthChildren[i].health);
        }
    }

    public void TakeDamage(float damage) {
        if (!isModuleHealth) {
            BaseCurrentHealth -= damage;

            if (health.CurrentHealth <= 0) Destroy(gameObject);
        }
        else if (isModuleHealth) {
            if (health.CurrentHealth >= damage) {
                health.CurrentHealth -= damage;
                ParentHealth.TakeDamage(damage);
            }
            else {
                float leftoverDamage = 0;

                leftoverDamage = damage - health.CurrentHealth;
                health.CurrentHealth = 0;
                Critical = true;
                GetComponent<MeshRenderer>().sharedMaterial = DamagedMaterial;

                ParentHealth.TakeDamage(damage - leftoverDamage);
                Debug.Log("Damage taken: " + (damage - leftoverDamage));

                if (leftoverDamage > 0) {
                    float critDamage = (float) Math.Round(leftoverDamage * CriticalMultiplier);
                    ParentHealth.TakeDamage(critDamage);
                    Debug.Log("Damage taken: " + critDamage + " (Crit)");
                }
            }
        }
    }

    public void CalculateHealth() {
        UpdateHealthList();
        if (!isModuleHealth) {
            health.MaxHealth = CalculateMaxHealth() + BaseMaxHealth;
            health.CurrentHealth = CalculateCurrentHealth() + BaseCurrentHealth;
        }
    }

    public float CalculateMaxHealth() {
        float health = 0;
        foreach (var module in Modules) {
            health += module.MaxHealth;
        }

        return health;
    }

    public float CalculateCurrentHealth() {
        float health = 0;
        foreach (var module in Modules) {
            health += module.CurrentHealth;
        }

        return health;
    }

    public void DamagePopup(string value) {
        GameObject popup = Instantiate(damagePopup, transform);
        popup.transform.localScale = popup.transform.localScale;
        popup.transform.localPosition = popupOffset;
        Text popupText = popup.GetComponentInChildren<Text>();
        popupText.color = Color.red;
        popupText.text = value;
        Destroy(popup, popupText.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private Color GetHealthBarColor(float value) {
        return gradient.Evaluate(1 - value / health.MaxHealth);
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class HealthBarAttribute : Attribute {
        public float MaxHealth { get; private set; }

        public HealthBarAttribute(float maxHealth) {
            this.MaxHealth = maxHealth;
        }
    }
}