using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour {

    [Header("Obstacle Destruction")]
    public GameObject explosionEffect;
    public AudioSource explosionAudio;

    [SerializeField] private float damage = 10.0f;

    public AudioSource crashAudio;


    public float Damage {
        get => damage;
        set => damage = value;
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            crashAudio.Play();

            // Deal damage
            Damageable d = other.GetComponent<Damageable>();
            if (d != null)
                d.Damage(damage);
        }
    }


    // Called when the obstacle is destroyed
    public void OnDeath() {
        
        // Instantient the explosition particles effect
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        
        explosionAudio.Play();
        Destroy(gameObject);
    }
}
