using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeImpact : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime, Camera.main.transform);
    }
}
