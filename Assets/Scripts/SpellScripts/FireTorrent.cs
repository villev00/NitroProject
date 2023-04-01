using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireTorrent : MonoBehaviour
{
    [SerializeField]
    Spell spell;
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f))
        {
            transform.LookAt(hit.point);
        }
        Destroy(this.gameObject, spell.spellDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
