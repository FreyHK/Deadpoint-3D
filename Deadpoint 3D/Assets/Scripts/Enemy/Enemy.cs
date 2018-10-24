using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Weapon weapon;
    public Health health;

    Transform playerTransform;

	void Start () {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        health.OnDeath += OnDeath;

        weapon.transform.position = transform.position;
        weapon.SetInActiveSlot(true);
        weapon.SetOnGround(false, Vector3.zero);
	}
	
	void Update () {
        //Point weapon at player
        weapon.transform.LookAt(playerTransform, Vector3.up);

        //Refill ammo supply
        if (weapon.CurAmmoSupply <= 0) {
            weapon.AddAmmo(weapon.data.AmmoCapacity);
        }

        //Shoot
        //Note: Weapon will automatically reload when out of ammo in clip
        weapon.TryShoot();
    }

    void OnDeath() {
        //Drop weapon
        weapon.transform.parent = null;
        weapon.transform.position = transform.position;
        weapon.SetOnGround(true, Vector3.zero);
    }
}
