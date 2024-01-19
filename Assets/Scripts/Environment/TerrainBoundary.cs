using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBoundary : MonoBehaviour {
    public static float leftSide = 100f;
    public static float rightSide = 900f;

    public float internalLeft;
    public float internalRight;


    // Update is called once per frame
    void Update() {
        internalLeft = leftSide;
        internalRight = rightSide;
    }
}
