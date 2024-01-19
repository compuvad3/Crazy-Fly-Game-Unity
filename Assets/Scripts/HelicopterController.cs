using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelicopterController : MonoBehaviour {

    private Rigidbody rb;

    AudioSource engineSound;

    [Header("Helicopter Movement Variables")]

    // How much fuel flows to the engine
    // In other words, the amount the throttle will increase/decrease
    [SerializeField] private float throttleAmount = 25f;
    [SerializeField] private float maxThrust = 50f;

    // Responsiveness will be set based on the difficulty selected by the player
    private float responsiveness;

    // A value between 0 and 100 which represents the amount of actual thrust (propulsive force of the helicopter)
    private float throttle;

    // Store the player's inputs
    private float roll;
    private float pitch;
    private float yaw;

    // Used to limit the helicopter movement at the beginning of the game
    public static bool canMove = false;

    [Space]

    [Header("Rotor Rotation Variables")]

    // Used to specify how much we modify the speed of the rotor
    [SerializeField] private float rotorSpeedModifier = 10f;

    [SerializeField] private Transform rotorsTransform;
    [SerializeField] private float dampingFactor = 1f;
    
    [Space]

    [Header("UI")]
    [SerializeField] private TMPro.TextMeshProUGUI distanceText;
    [SerializeField] private TMPro.TextMeshProUGUI distanceEndText;
    [SerializeField] private Image healthFillImage;
    [SerializeField] private GameObject deathScreenCanvas;
    [SerializeField] private GameObject defaultCanvas;

    [Space]

    [Header("Helicopter Destruction")]
    public GameObject explosionEffect;
    public AudioSource explosionAudio;

    [Space]

    [Header("Shooting")]
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform spawnBulletPosition;
    public AudioSource shootingAudio;

    private float distance;

    private Vector3 mouseWorldPosition;


    private void Awake() {
        rb = GetComponent<Rigidbody>();             // get a reference to the helicopter's rigid body component
        engineSound = GetComponent<AudioSource>();
    }


    private void Start() {
        responsiveness = DifficultyManager.instance.responsinevess;
    }


    // Update is called once per frame
    void Update() {
        
        // Move only if the helicopter can move
        if (canMove)
            HandleInputs();

        // Rotate the rotor
        rotorsTransform.Rotate(rotorSpeedModifier * (maxThrust * throttle) * Vector3.up);

        // Change the volume of the engine of the helicopter based on the throttle
        engineSound.volume = throttle * 0.01f;

        // Calculate the traveled distance and update it
        CalculateDistance();
        distanceText.SetText(distance.ToString("F0") + " m");
        distanceEndText.SetText(distance.ToString("F0") + " m");

        // Get the position in the Game where the mouse centered on the screen (crosshair) is pointing to
        // This will be the position where the projectile will arrive
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            mouseWorldPosition = raycastHit.point;
    }


    private void FixedUpdate() {

        // Check if the helicopter is below the maximum altitude
        if (transform.position.y < 300f) {

            // Add upward force to the helicopter
            rb.AddForce(transform.up * (maxThrust * throttle), ForceMode.Impulse);

            // Also add some forward force to the helicopter to make it move faster and increase the dynamics of the game
            rb.AddForce(transform.forward * (maxThrust * throttle /** forwardSpeedMultiplier*/), ForceMode.Impulse);
        }
        else {

            // Apply a damping force to stabilize the helicopter when at the maximum altitude
            rb.AddForce(-rb.velocity * dampingFactor, ForceMode.Acceleration);
        }

        // The same thing as above but for the rotational values
        rb.AddTorque(-transform.forward * roll * (responsiveness / 10f));
        rb.AddTorque(transform.right * pitch * responsiveness);
        rb.AddTorque(transform.up * yaw * responsiveness);
    }


    private void HandleInputs() {

        // Get the inputs from the player
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        // Handle how much thrust we are getting from the engine
        // If space is pressed, add the throttle amount to the throttle over the time space is pressed
        // Else if the left shift is pressed, remove the amount of throttle from the throttle
        if (Input.GetButton("Throttle Increase"))
            throttle += Time.deltaTime * throttleAmount;
        else if (Input.GetButton("Throttle Decrease"))
            throttle -= Time.deltaTime * throttleAmount;

        // Make sure that throttle is between 0 and 100 since it represents the percentage of the max amount of thrust the helicopter can achieve
        throttle = Mathf.Clamp(throttle, 0f, 100f);

        // If Fire1 pressed, instantiate a bullet from the position of the bullet spawn that will travel to the position in the Game where the crosshair points to (calculated in the Update() method)
        if (Input.GetButton("Fire1")) {
            Vector3 aimDirection = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            shootingAudio.Play();
            Instantiate(bullet, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
        }
    }


    // Calculate the distance traveled by the helicopter
    private void CalculateDistance() {
        distance += rb.velocity.magnitude * Time.deltaTime;     // distance = speed * time
    }


    // Called whenever the Helicopter is damaged
    public void OnDamage() {
        Damageable damageable = GetComponent<Damageable>();

        // Get the percentage of filled health image based on the current health and default total health
        healthFillImage.fillAmount = damageable.Health / damageable.DefaultHealth;
    }


    // Called when the Player dies
    public void OnDeath() {

        // Ensure that the DifficultyManager instance is destroyed when the Player dies
        DifficultyManager.instance = null;

        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionAudio.Play();
        canMove = false;

        // Turn off the default canvas
        defaultCanvas.SetActive(false);

        // Show the death screen
        deathScreenCanvas.SetActive(true);

        // Unable the player to move
        enabled = false;

        Destroy(gameObject);
    }
}
