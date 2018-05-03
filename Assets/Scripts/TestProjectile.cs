using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour {
    public GameObject target;
    public float speed = 1;
    public int damage;

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Hit! " + other.gameObject.name);
        if (other.gameObject.GetComponent<Health>()) {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        //Debug.Log("Hit! " + other.collider.gameObject.name);
        GetComponent<Collider>().isTrigger = true;
        if (other.collider.gameObject.GetComponent<Health>()) {
            other.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}