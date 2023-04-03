using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField]
    Spell spell;


    GameObject target;
  
    void Start()
    {
        target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, 1, QueryTriggerInteraction.Ignore))
        {
            target = hit.transform.gameObject;
            transform.position = hit.point;
        }
        transform.parent = target.transform;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<MagneticGrasp>() != null)
        {
            Debug.Log("other magnetic " + other.name);
            target = other.gameObject;
         
        }
    }
}
