using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject {

    public Inventory.InventorySlot inventorySlot = Inventory.InventorySlot.Primary;

    public int FireDamage = 30;
    public float FireDelay = .1f;
    public float FireRange = 999f;

    public int AmmoClipSize = 30;
    public int AmmoCapacity = 120;

    public float ReloadDelay = 1f;

    //TODO: ammo type data


    public string IdleAnimationName = "AK47_Idle";
    public string FireAnimationName = "AK47_Shoot";
    public string ReloadAnimationName = "AK47_Reload";

    [Space(10f)]

    public Sprite ItemIcon;

    //TODO sounds: shoot, reload, empty clip shoot sound
}
