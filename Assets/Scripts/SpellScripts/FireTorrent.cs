using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTorrent : MonoBehaviour
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
