using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour {

    public int MaxHealth = 100;

    public int CurrentHealth { get; private set; }

    public bool DestroyOnDeath = true;

    public Action<int, Vector3> OnTakeDamage;
    public Action OnDeath;

    private void Awake() {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int dmg) {
        if (IsDead())
            return;

        CurrentHealth -= dmg;

        if (IsDead()) {
            //DIE
            Die();
        }else {
            if (OnTakeDamage != null)
                OnTakeDamage(dmg, transform.position);
        }
    }

    void Die () {
        if (OnDeath != null)
            OnDeath();

        if (DestroyOnDeath)
            Destroy(gameObject);
    }

    public bool IsDead () {
        return CurrentHealth <= 0;
    }
}
