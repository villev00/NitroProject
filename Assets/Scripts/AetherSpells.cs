using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherSpells : MonoBehaviour
{
    public SpellUI spellUI;
    [SerializeField]
    Spell[] aetherSpells;
    // Start is called before the first frame update
    void Start()
    {
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            spellUI.ChangeSpellSet(aetherSpells);
        }
    }
    public void MagneticGrasp()
    {
        Debug.Log("Magnetic Grasp  used");
    }
    public void AethericLeap()
    {
        Debug.Log("Aetheric Leap used");
    }
    public void BlackHole()
    {
        Debug.Log("Black Hole used");
    }
}
