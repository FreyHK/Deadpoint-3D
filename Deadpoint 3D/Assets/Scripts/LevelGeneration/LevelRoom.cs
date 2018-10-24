using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration {
    public class LevelRoom : MonoBehaviour {

        public LevelDoor Entrance;
        public LevelDoor[] ExitDoors;

        public BoxCollider RoomCheck;
        public LayerMask RoomCheckMask;

        /// <summary>
        /// Are we overlapping another room?
        /// </summary>
        public bool IsOverlappingRoom () {
            Collider[] cols = Physics.OverlapBox(RoomCheck.transform.position, RoomCheck.size / 2 - Vector3.one * 1f, RoomCheck.transform.rotation, RoomCheckMask);

            for (int i = 0; i < cols.Length; i++) {
                if (cols[i].gameObject != RoomCheck.gameObject) {
                    //print("Hit other: " + cols[i].gameObject.name);
                    return true;
                }
            }
            return false;
        }

        public void ConnectEntranceToDoor (LevelDoor door) {
            //Move position of entire room to fit door's position
            Vector3 offset = transform.position - Entrance.transform.position;
            transform.position = door.transform.position + offset;
            transform.rotation = door.transform.rotation;
        }
    }
}