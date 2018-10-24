using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelGeneration;

[System.Serializable]
public class WeightedGameObjectList {

    [System.Serializable]
    public class ListElement {
        public GameObject Value;
        public int Weight = 1;
    }

    public List<ListElement> Elements;
    
    public GameObject GetRandomElement () {
        int sum = 0;
        for (int i = 0; i < Elements.Count; i++) {
            sum += Elements[i].Weight;
        }

        GameObject go = null;
        int index = Random.Range(0, sum);
        int current = 0;

        for (int i = 0; i < Elements.Count; i++) {
            current += Elements[i].Weight;
            if (index < current) {
                go = Elements[i].Value;
                break;
            }
        }
        return go;
    }
}
