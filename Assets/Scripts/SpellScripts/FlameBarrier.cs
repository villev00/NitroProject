using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FlameBarrier : MonoBehaviour
{
    [SerializeField]
    Spell spell;

    [SerializeField]
    float barrierHealth;

    void Start()
    {
        transform.position = transform.root.position;
        Destroy(gameObject, spell.spellDuration);
        //  transform.root.GetComponent<InsertPlayerScriptHere>().barrier = gameObject; 
        //InsertPlayerScriptHere: TakeDamage(float amount) if(barrier!=null) barrier.AbsorbDamage(amount); else reducehp(amount);
    }

    public void AbsorbDamage(float damage)
    {
        barrierHealth -= damage;
        if (barrierHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
