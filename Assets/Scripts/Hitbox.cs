using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    public GameObject Box;
    public string OwnerTag;

    public float Damage;
    public Element Element;
    public float ElementStrength;

    public float Lifetime = 5;


    public bool hit;

    private Collider collider;

    private void OnTriggerEnter(Collider other) {
        if (other.tag != OwnerTag && other.GetComponent<Health>() && !hit) {
            other.GetComponent<Health>().TakeDamage(Damage);
            //TODO: implement elemental damage
            hit = true;
        }
    }

    private void Start() {
        Destroy(gameObject, Lifetime);
    }
}