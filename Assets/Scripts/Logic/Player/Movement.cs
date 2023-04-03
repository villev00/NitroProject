using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace logic
{
    public class Movement : MonoBehaviour
    {
        public Vector3 GetDirection(float xDir, float zDir)
        {
            Vector3 direction = new Vector3();
            float angle = Mathf.Atan2(xDir, zDir) * Mathf.Rad2Deg;
            direction = Quaternion.Euler(0, angle + transform.eulerAngles.y, 0) * Vector3.forward;
            return direction;
        }
        //public Vector3 ResetDirection()
        //{
        //    Vector3 vector = new Vector3(0, 0, 0);
        //
        //    return vector; 
        //}
        public Vector3 MovePlayer(float x, float z, Vector3 orientationForward, Vector3 orientationRight)
        {
            //float verticalSpeed = 0;
            //Vector3 direction = new Vector3(x, 0, z);
            Vector3 moveDirection;// = ResetDirection();
            moveDirection = orientationForward * z + orientationRight * x; //GetDirection(x, z);

            return moveDirection;

            //moveDirection.y = verticalSpeed;
           
        }

    }

}
