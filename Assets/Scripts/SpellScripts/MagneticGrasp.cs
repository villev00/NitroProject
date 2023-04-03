using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MagneticGrasp : MonoBehaviour
{
    [SerializeField]
    Spell spell;

   
    GameObject target;
    bool magnetEnabled;
    // Start is called before the first frame update
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
        Invoke("DestroyGrasp", 5);
    }
    private void Update()
    {
        if (magnetEnabled)
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
        }
    }
    
    void DestroyGrasp()
    {
        if (!magnetEnabled)
            Destroy(gameObject);
    }
}
