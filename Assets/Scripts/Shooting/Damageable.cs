using UnityEngine.Events;
using UnityEngine;

public class Damageable : MonoBehaviour {
    [SerializeField] private float defaultHealth = 100.0f;

    [Header("Events")]
    [SerializeField] private UnityEvent onDeathEvent;
    [SerializeField] private UnityEvent onDamageEvent;

    public float DefaultHealth { get => defaultHealth; }
    public float Health { get; private set; }

    public static bool immortal = false;


    private void Start() {
        Health = defaultHealth;        
    }


    // Computes the damage taken
    public void Damage(float damage) {

        // Make sure that if the Player is dead, do not take any more damage!
        if (Health <= 0)
            return;

        // Make sure that if damage is greater than 0, we do not increase the health!
        if (damage < 0)
            return;

        onDamageEvent.Invoke();

        // If immortal is true, the damage taken should be 0
        if (immortal)
            damage = 0;

        // New health after damage received
        float updatedHealth = Health - damage;

        // Make sure it does not go below 0!
        if (updatedHealth <= 0) {
            updatedHealth = 0;      // this means death

            onDeathEvent.Invoke();
            
            enabled = false;        // makes sure we do not invoke the onDeath event again and again
        }

        // Update health
        Health = updatedHealth;
    }
}
