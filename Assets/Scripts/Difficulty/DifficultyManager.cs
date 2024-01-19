using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {
    public static DifficultyManager instance;

    public uint responsinevess;


    private void Awake() {

        // Ensure only one instance of AmmoManager exists
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
