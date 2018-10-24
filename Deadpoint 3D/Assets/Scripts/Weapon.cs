using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public enum State {
        Idle,
        Shooting,
        Reloading
    }

    public State curState { get; private set; }

    public WeaponData data;
    bool active = false;

    //How much ammo do we have in total
    public int CurAmmoSupply { get; private set; }
    //How much ammo left in clip
    public int CurAmmoInClip { get; private set; }
    
    [HideInInspector] public Animator anim;

    void Start () {
        curState = State.Idle;
        if (data == null)
            return;

        CurAmmoInClip = data.AmmoClipSize;
        CurAmmoSupply = data.AmmoCapacity;
        reloadCooldown = data.ReloadDelay;
        shootCooldown = 0f;

        ItemSprite.sprite = data.ItemIcon;
    }

    //Are we in an active slot? Yes - we can shoot.
    public void SetInActiveSlot (bool s) {
        active = s;
    }
    
    public Collider ItemCollider;
    public SpriteRenderer ItemSprite;
    public Rigidbody ItemBody;

    float dropForce = 10f;

    public void SetOnGround (bool s, Vector3 lookDirection) {
        ItemCollider.enabled = s;
        ItemSprite.enabled = s;

        if (s) {
            ItemBody.isKinematic = false;
            ItemBody.AddForce(lookDirection * dropForce, ForceMode.Impulse);
            active = false;
        } else
            ItemBody.isKinematic = true;
    }

    public void AddAmmo (int a) {
        CurAmmoSupply += a;
    }

    bool shootInput = false;

    public void TryShoot() {
        shootInput = true;
    }

    public void TryReload() {
        if (CurAmmoInClip < data.AmmoClipSize && CurAmmoSupply > 0)
            curState = State.Reloading;
    }

    void Update () {
        //No data = no weapon equipped, leave
        if (data == null || !active) {
            return;
        }
        
        switch (curState) {
            case State.Idle:
                State_Idle();
                break;
            case State.Shooting:
                State_Shooting();
                break;
            case State.Reloading:
                State_Reloading();
                break;
        }
        //Reset flag
        shootInput = false;
    }

    void State_Idle () {
        if (anim != null)
            anim.Play(data.IdleAnimationName);
        
        //Do we want to shoot?
        if (shootInput && CurAmmoInClip > 0) {
            curState = State.Shooting;
            return;
        }
        
        //Reloading triggered by: No ammo left
        if (CurAmmoInClip <= 0 && CurAmmoSupply > 0) {
            curState = State.Reloading;
            return;
        }
    }

    float shootCooldown = 0f;

    void State_Shooting() {
        if (anim != null)
            anim.Play(data.FireAnimationName);
        
        shootCooldown -= Time.deltaTime;

        if (shootCooldown <= 0f) {
            shootCooldown = data.FireDelay;
            //Do the pew pew
            Shoot();
        }
        
        //Are we done shooting?
        if (!shootInput || CurAmmoInClip <= 0) {
            curState = State.Idle;
            return;
        }
    }

    void Shoot () {
        //No ammo left
        if (CurAmmoInClip <= 0f) {
            //print(gameObject.name + ": Out of ammo");
            //TODO make 'click' sound
            return;
        }

        CurAmmoInClip--;

        /*
        //print(gameObject.name + ": Shooting");
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, data.FireRange);

        for (int i = 0; i < hits.Length; i++) {
            //If we hit ourselves, continue
            if (hits[i].collider.gameObject == transform.root.gameObject)
                continue;

            Health e = hits[i].collider.GetComponent<Health>();
            if (e) {
                e.TakeDamage(data.FireDamage);
                //We only want to hit one
                break;
            }
        }
        */
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, data.FireRange)) {
            print("Hit: " + hit.collider.name);
            Health e = hit.collider.GetComponent<Health>();
            if (e) {
                e.TakeDamage(data.FireDamage);
            }
        }
    }

    float reloadCooldown;

    void State_Reloading() {
        if (anim != null)
            anim.Play(data.ReloadAnimationName);

        reloadCooldown -= Time.deltaTime;
        if (reloadCooldown <= 0f) {
            //Can only reload as much ammo as we have in supply
            int amount = Mathf.Min(CurAmmoSupply, data.AmmoClipSize) - CurAmmoInClip;
            CurAmmoSupply -= amount;
            CurAmmoInClip += amount;

            reloadCooldown = data.ReloadDelay;

            curState = State.Idle;
        }
    }
}
