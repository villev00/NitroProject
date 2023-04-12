using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class portal : MonoBehaviour
{
    
    public Transform playerCamera;
    public Transform portalB;
    public Transform PortalA;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - PortalA.position;
        transform.position = portalB.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portalB.rotation, PortalA.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }

}

