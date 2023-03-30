using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpells : MonoBehaviour
{
    public SpellUI spellUI;
    [SerializeField]
    Spell[] lightningSpells;
    // Start is called before the first frame update
    void Start()
    {
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            spellUI.ChangeSpellSet(lightningSpells);
        }
    }

    public void StaticField()
    {
        Debug.Log("Static Field used");
    }
    public void TempestSurge()
    {
        Debug.Log("Tempest Surge used");
    }
    public void ChainsOfLightning()
    {
        Debug.Log("Chains of Lightning used");
    }
}
