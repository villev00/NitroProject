using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField]
    Spell spell;


    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, 1, QueryTriggerInteraction.Ignore))
        {
            transform.position = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
            transform.eulerAngles = new Vector3(-90, 0, 0);
            transform.parent = null;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<MagneticGrasp>() != null)
        {
            Debug.Log("other magnetic " + other.name);
           
         
        }
    }
}
