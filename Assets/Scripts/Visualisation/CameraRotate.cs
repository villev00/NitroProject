using UnityEngine;
using System;

public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 1000;

    [SerializeField]
    Transform orientation;

    [SerializeField]
    Transform player;

    float mouseX;
    float mouseY;
    float xRotation;
    float yRotation;

    Vector3 offset = new Vector3(0, 1.5f, 0);
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // Camera rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        // Player model rotation
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
        // Camera position
        transform.position = player.transform.position + orientation.forward / 2 + offset;
    }
}
