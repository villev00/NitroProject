using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireTorrent : MonoBehaviour
{
    [SerializeField]
    Spell spell;
    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();
    bool onCooldown;
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f))
        {
            transform.LookAt(hit.point);
        }
        Destroy(this.gameObject, spell.spellDuration);
        InvokeRepeating("Damage", 0, 1);
    }
    private void OnTriggerEnter(Collider other)
    {
 
        if (other.CompareTag("Enemy"))
        {
            if (!enemies.Contains(other.gameObject))
            {
                enemies.Add(other.gameObject);
                Debug.Log("In damage zone: "+other.name); }
        }
       
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            if (enemies.Contains(other.gameObject))
            {
                enemies.Remove(other.gameObject);
                Debug.Log("Enemy left damage zone: " + other.name);
            }
        }

    }
    void Damage()
    {
        foreach(GameObject enemy in enemies)
        {
            if (enemy != null)
                Debug.Log("Damaged " + enemy.name);
             //   enemy.GetComponent<Enemy>().TakeDamage(spell.spellDamage);
        }
    }
}
