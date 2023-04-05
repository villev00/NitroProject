using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
//Chains of lightning is an area of effect type of skill. The spell will
//hit all enemies in close range of the target. Enemies will get
//stunned and take damage.  Stun duration is 5 seconds and 
//damage amount is 20. Mana cost is 15 and the cooldown is 20
//seconds.

public class ChainsOfLightning : MonoBehaviour
{
    Vector3 target;
    [SerializeField]
    float spellMovementSpeed;
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, 1))
        {
            target = hit.point;
        }
        transform.parent = null;

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, spellMovementSpeed * Time.deltaTime);
    }
}
