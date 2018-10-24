using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration {
    public class LevelGenerator : MonoBehaviour {

        public enum TileType {
            Empty,
            Wall,
            Door
        }

        
        void Start() {
            GenerateLevel();
            //StartCoroutine(GenerateLevel());
        }
        
        //public LevelRoom[] RoomPrefabs;

        public WeightedGameObjectList RoomPrefabs;

        public LevelRoom StartingRoom;

        int MaxRooms = 12;

        void GenerateLevel () {
            int roomCount = 0;

            List<LevelDoor> roomExits = new List<LevelDoor>();
            foreach (LevelDoor d in StartingRoom.ExitDoors) {
                roomExits.Add(d);
            }

            while (roomCount < MaxRooms) {
                //Increment first to avoid infinite loop.
                roomCount++;

                LevelRoom room = CreateRoom();
                int exitIndex = -1;

                for (int i = 0; i < roomExits.Count; i++) {
                    //Try to place here
                    room.ConnectEntranceToDoor(roomExits[i]);
                    if (!room.IsOverlappingRoom()) {
                        //We can stay here.
                        exitIndex = i;
                        break;
                    }
                    //yield return new WaitForSeconds(1f);
                }
                if (exitIndex == -1) {
                    //We didn't find space for this room, scrap it
                    Destroy(room.gameObject);
                    continue;
                }

                roomExits[exitIndex].SetState(true);

                //Add all new exits to list of possible doors
                foreach (LevelDoor d in room.ExitDoors) {
                    roomExits.Add(d);
                }
                //Remove from list of possible exits
                roomExits.RemoveAt(exitIndex);

                //yield return new WaitForSeconds(1f);
            }

            //TODO create final level exit

            for (int i = 0; i < roomExits.Count; i++) {
                //Block off exits that are unused
                roomExits[i].SetState(false);
            }
        }

        LevelRoom CreateRoom () {
            GameObject prefab = RoomPrefabs.GetRandomElement();
            LevelRoom room = Instantiate(prefab, transform).GetComponent<LevelRoom>();

            return room;
        }
    }
}
