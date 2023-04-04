
using UnityEngine;

//Flame barrier is a protective spell that absorbs damage.
//Absorbable damage amount is 100. The spell lasts for 5 seconds. 
//The mana cost is 15 and the cooldown is 45 seconds.
public class FlameBarrier : MonoBehaviour
{
    [SerializeField]
    Spell spell;
    [SerializeField]
    int barrierHealth;
  
    void Start()
    {
        //Move objects location to players location (from spawnpoint)
        transform.position = transform.root.position;
        Destroy(gameObject, spell.spellDuration);
        //Let healthlogic know flamebarrier is up and set shield amount
        transform.root.GetComponent<HealthLogic>().flameBarrier = gameObject;
        transform.root.GetComponent<HealthLogic>().shield = barrierHealth;
    }
   
    private void OnDestroy()
    {
        transform.root.GetComponent<HealthLogic>().shield = 0;
    }
}
