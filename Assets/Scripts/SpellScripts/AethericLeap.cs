using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AethericLeap : MonoBehaviour
{
    Vector3 target;

    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f))
        {
            target = hit.point;
        }
      
        transform.position = target;
        StartCoroutine(Teleport());
    }
 
    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.2f);
        transform.root.position = target;
       
        Destroy(gameObject);
    }
}
