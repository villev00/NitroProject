using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FlameBarrier : MonoBehaviour
{
    [SerializeField]
    Spell spell;
    void Start()
    {
        Destroy(this.gameObject, spell.spellDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
