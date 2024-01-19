using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour {

    [Header("Destinations")]
    [SerializeField] GameObject[] destinations;

    [Space]

    [Header("Movement speed")]
    [SerializeField] float speed = 1f;

    // Store the index corresponding the current destination the obstacle is moving towards
    int currentDestination = 0;

    // Update is called once per frame
    void Update() {
        

        // Check if the distance between the current position of the obstacle and the current destination is less than 0.1
        // If it is true, change to the next destination
        // Note that if we reach the end of the destinations array, restart from the 1st destination! 
        if (Vector3.Distance(transform.position, destinations[currentDestination].transform.position) < 0.1f) {
            
            currentDestination++;

            if (currentDestination == destinations.Length)
                currentDestination = 0;
        }

        // Update the position of the obstacle
        transform.position = Vector3.MoveTowards(transform.position, destinations[currentDestination].transform.position, speed * Time.deltaTime);
    }
}
