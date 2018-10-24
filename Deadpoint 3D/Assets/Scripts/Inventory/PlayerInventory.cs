using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory {

    public enum InventorySlot {
        Primary,
        Secondary,
        Melee //Grenade, health kit, etc...
    }
    /// <summary>
    /// Handles weapon switching and swapping/equipping weapons
    /// </summary>
    public class PlayerInventory : MonoBehaviour {
        
        public Animator[] weaponAnimators = new Animator[3];

        Weapon[] playerWeapons = new Weapon[3];
        int activeWeapon = 0;

        void Start() {
            UpdateWeaponVisuals();
        }
        
        void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                SetActiveWeapon(InventorySlot.Primary);

            }else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                SetActiveWeapon(InventorySlot.Secondary);

            }else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                SetActiveWeapon(InventorySlot.Melee);

            }else if (Input.GetKeyDown(KeyCode.Q)) {
                DropWeapon(activeWeapon);
            }
        }

        public Weapon GetActiveWeapon () {
            return playerWeapons[activeWeapon];
        }

        void SetActiveWeapon (InventorySlot slot) {
            activeWeapon = (int)slot;

            UpdateWeaponVisuals();
        }

        void UpdateWeaponVisuals () {
            for (int i = 0; i < playerWeapons.Length; i++) {
                if (playerWeapons[i] != null)
                    playerWeapons[i].SetInActiveSlot(i == activeWeapon);

                weaponAnimators[i].gameObject.SetActive(i == activeWeapon && playerWeapons[i] != null);
            }
        }

        void EquipWeapon (Weapon weapon) {
            int i = (int)weapon.data.inventorySlot;

            if (playerWeapons[i] != null) {
                print("Already equipped a weapon, dropping");
                DropWeapon(i);
            }

            //Place correctly in hierarchy
            weapon.SetOnGround(false, Vector3.zero);
            weapon.transform.parent = transform;
            weapon.transform.position = transform.position;
            weapon.transform.rotation = transform.rotation;

            weapon.anim = weaponAnimators[i];
            playerWeapons[i] = weapon;

            //Switch to weapon if we aren't holding anything
            if (playerWeapons[activeWeapon] == null)
                SetActiveWeapon(weapon.data.inventorySlot);
            else
                UpdateWeaponVisuals();
        }

        void DropWeapon(int i) {
            //Can't drop nothing or a weapon that is reloading
            if (playerWeapons[i] == null || playerWeapons[i].curState == Weapon.State.Reloading)
                return;

            playerWeapons[i].SetOnGround(true, transform.forward);

            //Place correctly in hierarchy
            playerWeapons[i].transform.parent = null;
            playerWeapons[i].transform.position = transform.position - transform.up * .5f;

            playerWeapons[i] = null;
            
            UpdateWeaponVisuals();
        }

        //Pickup up items and weapons
        private void OnTriggerEnter(Collider other) {
            
            if (other.tag == "ItemPickup") {

            }else if (other.tag == "WeaponPickup") {
                Weapon weapon = other.GetComponent<Weapon>();
                EquipWeapon(weapon);
            }
        }
    }
}
