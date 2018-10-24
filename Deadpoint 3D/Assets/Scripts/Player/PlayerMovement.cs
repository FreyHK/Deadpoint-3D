using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Movement
    public Rigidbody body;
    public float MoveSpeed = 12f;

    //Mouse look
    public float MouseSensitivity = 100f;
    float rotY;

    private void Start() {
        //FIXME maybe this shouldnt be handled here.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotY = transform.rotation.eulerAngles.y;
    }

    void FixedUpdate () {
        body.velocity = Vector3.zero;
        MouseLook();
        MovePosition();
    }

    void MouseLook () {
        rotY += Input.GetAxis("Mouse X") * MouseSensitivity * Time.fixedDeltaTime;
        Quaternion rot = Quaternion.Euler(0f, rotY, 0f);
        transform.rotation = rot;
    }

    void MovePosition () {
        Vector3 forward = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 strafe = transform.right * Input.GetAxisRaw("Horizontal");

        Vector3 move = (forward + strafe).normalized * MoveSpeed;
        body.MovePosition(body.position + move * Time.fixedDeltaTime);
    }
}
