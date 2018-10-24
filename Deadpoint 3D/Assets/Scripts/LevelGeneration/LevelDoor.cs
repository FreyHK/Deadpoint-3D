using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration {
    public class LevelDoor : MonoBehaviour {

        //Set in Unity
        public GameObject WallVisual;

        public void SetState (bool IsDoor) {
            if (IsDoor) {
                WallVisual.SetActive(false);
                //Do other stuff
            } else {
                WallVisual.SetActive(true);
            }
        }
    }
}
