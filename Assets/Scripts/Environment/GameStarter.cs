using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStarter : MonoBehaviour {

    [Header("UI")]
    [SerializeField] private GameObject countDown3;
    [SerializeField] private GameObject countDown2;
    [SerializeField] private GameObject countDown1;
    [SerializeField] private GameObject countDownGo;
    [SerializeField] private GameObject fadeIn;

    [Header("Audio")]
    [SerializeField] private AudioSource readyFX;
    [SerializeField] private AudioSource goFX;


    // Start is called before the first frame update
    void Start() {
        StartCoroutine(CountSequence());
    }
    
    IEnumerator CountSequence() {

        // Wait for 2 seconds so that the fade in is done, before starting counting down
        yield return new WaitForSeconds(2f);

        // Wait for 1 second between each element of the count down
        countDown3.SetActive(true);

        // Play the count down sfx as the number appears on the screen
        readyFX.Play();

        yield return new WaitForSeconds(0.5f);
        countDown2.SetActive(true);
        readyFX.Play();

        yield return new WaitForSeconds(0.5f);
        countDown1.SetActive(true);
        readyFX.Play();

        yield return new WaitForSeconds(0.5f);
        countDownGo.SetActive(true);

        // Play the go sfx as the number appears on the screen
        goFX.Play();

        // Allow the helicopter to move only when the count down completed
        HelicopterController.canMove = true;
    }
}
