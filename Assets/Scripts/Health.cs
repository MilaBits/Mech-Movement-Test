using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = System.Random;

[ExecuteInEditMode]
public class Health : MonoBehaviour {
    [BoxGroup("Health")]
    public bool isModuleHealth;

    [BoxGroup("Health")] public HealthValue health;

    [HideIf("isModuleHealth"), HorizontalGroup("Health/Base"), LabelWidth(90), LabelText("Base Health"),SuffixLabel("HP",true)]
    public int BaseCurrentHealth;

    [HideIf("isModuleHealth"), HorizontalGroup("Health/Base"), LabelWidth(30), LabelText("Max"), SuffixLabel("HP",true)]
    public int BaseMaxHealth;

    [BoxGroup("Health/UI")]
    [HideIf("isModuleHealth"), HorizontalGroup("Health/UI/UI"), LabelWidth(30), LabelText("Bar")]
    public RectTransform UIBar;
    [HideIf("isModuleHealth"), HorizontalGroup("Health/UI/UI"), LabelWidth(35), LabelText("Text")]
    public Text UIText;
    
    [BoxGroup("Health"), ShowIf("isModuleHealth")]
    public Health ParentHealth;

    [BoxGroup("Health"), HideIf("isModuleHealth")]
    public List<HealthValue> Modules;

    [BoxGroup("Health"), Button("Update List", ButtonSizes.Medium), HideIf("isModuleHealth")]
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
        }

        health.Owner = gameObject.name;
    }

    private void Update() {
        if (ParentHealth) ParentHealth.CalculateHealth();

        if (!isModuleHealth) {
            UIBar.localScale = new Vector3(ExtensionMethods.Remap(health.CurrentHealth, 0, health.MaxHealth, 0, 1), 1, 1);
            UIText.text = String.Format("{0} / {1}", health.CurrentHealth, health.MaxHealth);
        }
    }

    public void TakeDamage(int damage) {
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
                int leftoverDamage = 0;

                leftoverDamage = damage - health.CurrentHealth;
                health.CurrentHealth = 0;
                Critical = true;
                GetComponent<MeshRenderer>().sharedMaterial = DamagedMaterial;

                ParentHealth.TakeDamage(damage - leftoverDamage);
                Debug.Log("Damage taken: " + (damage - leftoverDamage));

                if (leftoverDamage > 0) {
                    int critDamage = (int) Math.Round(leftoverDamage * CriticalMultiplier);
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

    public int CalculateMaxHealth() {
        int health = 0;
        foreach (var module in Modules) {
            health += module.MaxHealth;
        }

        return health;
    }

    public int CalculateCurrentHealth() {
        int health = 0;
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
        return Color.Lerp(Color.red, Color.green, Mathf.Pow(value / health.MaxHealth, 2));
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class HealthBarAttribute : Attribute {
        public float MaxHealth { get; private set; }

        public HealthBarAttribute(float maxHealth) {
            this.MaxHealth = maxHealth;
        }
    }
}