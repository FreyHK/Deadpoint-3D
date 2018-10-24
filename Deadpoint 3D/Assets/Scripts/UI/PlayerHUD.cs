using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;

public class PlayerHUD : MonoBehaviour {

    public Health PlayerHealth;
    public Image HealthBarImage;

    public PlayerInventory inventory;
    public Text AmmoSupplyText;
    public Image CurrentWeaponImage;

    //TODO add inventory display

    void Start () {
		
	}
	
	void Update () {
        //Healthbar
        float p = (float)PlayerHealth.CurrentHealth / PlayerHealth.MaxHealth;
        HealthBarImage.fillAmount = p;




        Weapon w = inventory.GetActiveWeapon();
        //Ammo display
        if (w != null)
            AmmoSupplyText.text = w.CurAmmoInClip.ToString() + "/" + w.CurAmmoSupply.ToString();
        else
            AmmoSupplyText.text = "";

        //Display weapon icon
        if (w != null) {
            CurrentWeaponImage.enabled = true;
            CurrentWeaponImage.sprite = w.data.ItemIcon;
        } else {
            CurrentWeaponImage.enabled = false;
        }
    }
}
