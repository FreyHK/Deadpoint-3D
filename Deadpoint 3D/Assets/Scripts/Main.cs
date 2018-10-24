using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public Health PlayerHealth;
    
	void Start () {
        PlayerHealth.OnDeath += OnPlayerDied;
	}
	
	void Update () {
		
	}

    void OnPlayerDied () {
        print("Player died.");
    }
}
