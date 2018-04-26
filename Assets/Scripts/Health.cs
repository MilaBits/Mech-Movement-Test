using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Health : MonoBehaviour {
    public bool isModuleHealth;
    public Health ParentHealth;
    public int maxHealth;
    public int health;
    public bool Critical;
    public float CriticalMultiplier = 1.5f;


    [SerializeField] private GameObject damagePopup;
    [SerializeField] private Vector3 popupOffset;

    public Material DamagedMaterial;

    // Use this for initialization
    void Start() {
        if (isModuleHealth) {
            ParentHealth.health += health;
            ParentHealth.maxHealth = ParentHealth.health;
        }

        maxHealth = health;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (isModuleHealth) {
            if (Critical) {
                ParentHealth.health -= (int) Math.Round(damage * CriticalMultiplier);
            }
            else {
                ParentHealth.health -= damage;
            }
            DamagePopup("Module damaged!");
        }

//        if (Critical && isModuleHealth && ParentHealth != null) {
//            ParentHealth.TakeDamage((int) Math.Round(damage * CriticalMultiplier));
//            //TODO: popup text yelling "CRITICAL HIT!!!!!!!!"
//        }

        if (health <= 0) {
            if (ParentHealth) {
                Critical = true;
                GetComponent<MeshRenderer>().sharedMaterial = DamagedMaterial;
                DamagePopup("Module damaged!");
            }
            else Destroy(gameObject);
        }
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
}