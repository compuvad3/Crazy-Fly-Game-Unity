using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {

    // Store the sections to be generated
    public GameObject[] terrain;

    // Represent the number of the terrain to be generated
    public int terrNum;

    
    // Generate a new random terrain as the Player collides with the GameObject that will trigger the new generation!
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {

            terrNum = Random.Range(0, 2);       // randomly pick one of the terrains availabe for generation

            // Generate the new section
            // The z position of the newly generated terrain will be the position of z of the game object that triggered the instantiation plus 1000 since this is the lenght of each terrain
            Instantiate(terrain[terrNum], new Vector3(0, 0, (this.transform.position.z + 1000)), Quaternion.identity);
        }
    }
}
