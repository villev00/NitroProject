using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlazeImpact : MonoBehaviour
{
    Vector3 target;

    private void Start()
    {
       // target = new Vector3(Screen.height / 2, Screen.width / 2, transform.forward);
        
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 2 * Time.deltaTime);
    }
}
