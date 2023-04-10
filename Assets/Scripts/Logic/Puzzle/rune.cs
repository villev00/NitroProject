using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune : MonoBehaviour
{
    [SerializeField]
    public GameObject _rune;
    
    Spell spell;
    
    private void Start()
    {
        spell = GameObject.Find("Spell").GetComponent<Spell>();
    }
    
   
    
}
