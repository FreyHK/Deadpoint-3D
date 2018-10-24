using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudEffects : MonoBehaviour {
    
    public Health PlayerHealth;
    
    //Gets 'redder' the more hurt the player is.
    public Image HealthOverlayImage;

    //Flashes red when player gets hurt.
    public Image HurtOverlayImage;

    void Start () {
        PlayerHealth.OnTakeDamage += OnPlayerTakeDamage;
    }

    //At what percentage health does the overlay start showing?
    float healthOverlayStart = .2f;

	void Update () {
        if (PlayerHealth.IsDead())
            return;

        float p = (float)PlayerHealth.CurrentHealth / PlayerHealth.MaxHealth;
        if (p <= healthOverlayStart) {
            HealthOverlayImage.enabled = true;

            float a = 1f - (p / healthOverlayStart * maxAlpha);

            //Apply new alpha
            Color c = HealthOverlayImage.color;
            HealthOverlayImage.color = new Color(c.r, c.g, c.b, a);

        } else {
            HealthOverlayImage.enabled = false;
        }
	}

    void OnPlayerTakeDamage (int dmg, Vector3 pos) {
        StopAllCoroutines();
        StartCoroutine(HurtOverlayFade());
    }

    float maxAlpha = 180f / 255f;
    //Fade in for .05 sec
    float fadeInDur = 0.1f;
    float fadeOutDur = 0.2f;

    IEnumerator HurtOverlayFade () {
        HurtOverlayImage.enabled = true;

        float t = 0f;
        while (t <= fadeInDur) {
            float a = t / fadeInDur * maxAlpha;
            
            //Apply new alpha
            Color c = HurtOverlayImage.color;
            HurtOverlayImage.color = new Color(c.r, c.g, c.b, a);

            t += Time.deltaTime;
            //Wait a frame
            yield return null;
        }

        t = 0f;
        while (t <= fadeOutDur) {
            float a = (1f - t / fadeInDur) * maxAlpha;

            //Apply new alpha
            Color c = HurtOverlayImage.color;
            HurtOverlayImage.color = new Color(c.r, c.g, c.b, a);

            t += Time.deltaTime;
            //Wait a frame
            yield return null;
        }
        HurtOverlayImage.enabled = false;
    }
}
