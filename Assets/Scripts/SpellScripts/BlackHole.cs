using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Black hole is a spell that pulls enemies within a small range
//together but doesn’t do any damage. The mana cost is 15 and
//the cooldown is 15 seconds.
public class BlackHole : MonoBehaviour
{
    [SerializeField]
    Spell spell;
    bool triggerEffect;
    [SerializeField]
    float pullSpeed;
    List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, 1))
        {
            transform.position = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
            transform.eulerAngles = new Vector3(-90, 0, 0);
            transform.parent = null;
        }
        Destroy(gameObject, spell.spellDuration);
    }
    private void Update()
    {
        //After black hole is entered, start pulling enemies inside
        if (triggerEffect)
        {
            foreach(GameObject enemy in enemies)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, transform.position, pullSpeed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Add enemies in trigger zone to list, so their location can be accessed during pull effect
        if (other.CompareTag("Enemy") && !enemies.Contains(other.gameObject))
        {
           enemies.Add(other.gameObject);
           triggerEffect = true; 
        }

    }
    
}
