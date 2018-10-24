using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class PlayerWeaponInput : MonoBehaviour {

    public PlayerInventory Inventory;
    
	void Update () {
        Weapon w = Inventory.GetActiveWeapon();
        if (w == null)
            return;

        if (Input.GetMouseButton(0))
            w.TryShoot();

        if (w.curState == Weapon.State.Idle && Input.GetKeyDown(KeyCode.R)) {
            w.TryReload();
        }
    }
}
