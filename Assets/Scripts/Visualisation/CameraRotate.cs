using UnityEngine;
using System;
using System.ComponentModel.Design.Serialization;
using Photon.Pun;

public class CameraRotate : MonoBehaviour
{
    PhotonView pv;
    [SerializeField]
    float sensitivity;

    [SerializeField]
    Transform orientation;

    //[SerializeField]
    //Transform player;

    [SerializeField]
    Transform cameraHolder;

    float mouseX;
    float mouseY;
    float xRotation;
    float yRotation;

    Vector3 offset = new Vector3(0, 1.5f, 0);

    private void Awake()
    {
        //pv = transform.parent.GetComponent<PhotonView>();
        //if (!pv.IsMine) Destroy(gameObject);
        //orientation = transform.parent.GetChild(1).transform;
        //player = transform.root;
        sensitivity = PlayerPrefs.GetFloat("sensitivity");
       
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        if(cameraHolder != null && orientation != null)
        {
            // Camera rotation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            // Player model rotation
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            // Camera position
            transform.position = cameraHolder.position;
            //transform.position = player.transform.position + orientation.forward / 2 + offset;
        }
    }

    public void FindPlayer(Transform orientationTransform, Transform cameraHolderTransform)
    {
        cameraHolder = cameraHolderTransform;
        orientation = orientationTransform;
    }
}
