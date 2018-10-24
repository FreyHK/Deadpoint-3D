using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardTransform : MonoBehaviour {

    Transform mainCam;

	void Start () {
        mainCam = Camera.main.transform;
	}
	
	void Update () {
        if (mainCam != null)
            transform.LookAt(mainCam, Vector3.up);
	}
}
