using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Magnetic grasp is a spell used on two enemies that will be pulled
//together. Enemies can also be pulled against a wall. Direct hit will
//deal 5 damage and the pull deals 10 additional damage. The 
//mana cost is 15 and the cooldown is 10 seconds.

public class MagneticGrasp : MonoBehaviour
{
    [SerializeField]
    Spell spell;
    GameObject target;
    bool magnetEnabled;
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
        //direct hit damage
        Invoke("DestroyGrasp", 5);
    }
    private void Update()
    {
        if (magnetEnabled) //TO DO WALL MAGNET
        {
            transform.root.position = Vector3.MoveTowards(transform.root.position, target.transform.position, 5*Time.deltaTime);
            target.transform.position = Vector3.MoveTowards(target.transform.position, transform.root.position, 5*Time.deltaTime);
            if(Vector3.Distance(transform.root.position, target.transform.position) < 0.1f)
            {
                Destroy(target.transform.GetComponentInChildren<MagneticGrasp>().gameObject);
                Destroy(gameObject);
            }
        }
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<MagneticGrasp>() != null)
        {
            Debug.Log("other magnetic " + other.name);
            target = other.gameObject;
            magnetEnabled = true;
            //pull damage
        }
    }
    
    void DestroyGrasp()
    {
        if (!magnetEnabled)
            Destroy(gameObject);
    }
}
