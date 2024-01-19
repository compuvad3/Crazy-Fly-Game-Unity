using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    [SerializeField] private float speed = 100.0f;
    [SerializeField] private float damage = 10.0f;

    public GameObject explosionEffect;

    private Rigidbody bulletRigidBody;

    public static bool moreDamageBullets = false;

    
    private void Awake() {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    private void Start() {
        bulletRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {

        // If bullets that deal more damage are equiped, more damage is done
        if (moreDamageBullets)
            damage = 50;

        // Deal damage
        Damageable d = other.GetComponent<Damageable>();
        if (d != null)
            d.Damage(damage);

        Destroy(gameObject);
    }
}
