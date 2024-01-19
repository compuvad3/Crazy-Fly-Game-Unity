using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectableControl : MonoBehaviour {

    [Header("Coins")]
    [SerializeField] private TMPro.TextMeshProUGUI coinText;
    [SerializeField] private TMPro.TextMeshProUGUI coinEndText;

    [Space]

    [Header("Shields")]
    [SerializeField] private GameObject shield;

    [Space]

    [Header("Powerful Bullets")]
    [SerializeField] private GameObject powerfulBullets;


    public static int coinCount;

    public static bool immortal = false;

    public static bool moreDamageBullets = false;


    // Update is called once per frame
    void Update() {

        // Update the text of the coins
        coinText.SetText(coinCount + "");
        coinEndText.SetText(coinCount + "");

        if (immortal) {
            StartCoroutine(ImmortalityTimer());
            immortal = false;
        }

        if (moreDamageBullets) {
            StartCoroutine(MoreDamageBulletsTimer());
            moreDamageBullets = false;
        }

    }

    
    private IEnumerator ImmortalityTimer() {
        Damageable.immortal = true;
        shield.SetActive(true);
        yield return new WaitForSeconds(15f);
        shield.SetActive(false);
        Damageable.immortal = false;
    }


    private IEnumerator MoreDamageBulletsTimer() {
        BulletProjectile.moreDamageBullets = true;
        powerfulBullets.SetActive(true);
        yield return new WaitForSeconds(60f);
        powerfulBullets.SetActive(false);
        BulletProjectile.moreDamageBullets = false;
    }
}
