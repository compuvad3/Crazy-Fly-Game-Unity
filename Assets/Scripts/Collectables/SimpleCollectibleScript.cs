using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {Shield, Coin, BulletUpgrade};
	public CollectibleTypes CollectibleType;		// this gameObject's type
	
	public bool rotate;								// do you want it to rotate?
	public float rotationSpeed;

	public AudioClip collectSound;
	public GameObject collectEffect;

	
	// Update is called once per frame
	void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			Collect ();
		}
	}


	public void Collect() {
		if(collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);

		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		if (CollectibleType == CollectibleTypes.Shield) {
			CollectableControl.immortal = true;
		}
		if (CollectibleType == CollectibleTypes.Coin) {

            // If a Coin type is collected, increase the total number of coins by 1
            CollectableControl.coinCount += 1; 
		}
		if (CollectibleType == CollectibleTypes.BulletUpgrade) {
            CollectableControl.moreDamageBullets = true;

            Debug.Log ("Do BulletUpgrade Command");
		}

		Destroy (gameObject);
	}
}
