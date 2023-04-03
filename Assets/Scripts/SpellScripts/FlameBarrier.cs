using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FlameBarrier : MonoBehaviour
{
    [SerializeField]
    Spell spell;

    [SerializeField]
    int barrierHealth;
  
    void Start()
    {
        
        transform.position = transform.root.position;
        Destroy(gameObject, spell.spellDuration);
        transform.root.GetComponent<HealthLogic>().flameBarrier = gameObject;
        transform.root.GetComponent<HealthLogic>().shield = barrierHealth;
     
    }
   
   
    private void OnDestroy()
    {
        transform.root.GetComponent<HealthLogic>().shield = 0;
    }
}
