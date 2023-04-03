using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


//Aetheric leap is a spell that works in a similar way like magnetic 
//grasp, but instead of pulling enemies you can pull yourself into a
//desired location. The mana cost is 5 and the cooldown is 5 seconds.
public class AethericLeap : MonoBehaviour
{
    Vector3 target;

    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f))
        {
            target = hit.point;
            transform.position = target;
            StartCoroutine(Teleport());
        }
    }
 
    IEnumerator Teleport()
    {
        //After a short while teleport to target location,
        //Delay is for the player to see the visual effect
        yield return new WaitForSeconds(0.2f);
        //Disable character controller or it prevents teleporting
        transform.root.gameObject.GetComponent<CharacterController>().enabled = false; 
        transform.root.position = target;
        transform.root.gameObject.GetComponent<CharacterController>().enabled = true;
        Destroy(gameObject);
    }
}
