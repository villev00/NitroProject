using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    Runes2 parent;
    void Start()
    {
        parent = transform.parent.GetComponent<Runes2>();
        for(int i=0; i < 3; i++)
        {
            if (transform.GetChild(i).transform.gameObject.activeInHierarchy)
            {
                gameObject.tag = transform.GetChild(i).tag;
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        parent.tag = gameObject.tag;
        parent.OnTriggerEnter(other);
    }
}
