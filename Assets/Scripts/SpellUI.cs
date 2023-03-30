using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    [SerializeField]
    GameObject [] spellSlots;

    [SerializeField]
    GameObject spellManager;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spellSlots[0].GetComponent<Button>().onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spellSlots[1].GetComponent<Button>().onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spellSlots[2].GetComponent<Button>().onClick.Invoke();
        }
    }

    public void ChangeSpellSet(Spell[] spells)
    {
        for (int i=0; i < spells.Length; i++)
        {
            spellSlots[i].GetComponent<Image>().sprite = spells[i].spellSprite;
            spellSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }
       SetSpells(spells[0].spellElement);
    }

    void SetSpells(Element element)
    {
        if (element == Element.Fire)
        {
            spellSlots[0].GetComponent<Button>().onClick.AddListener(spellManager.GetComponent<FireSpells>().BlazeImpact);
            spellSlots[1].GetComponent<Button>().onClick.AddListener(spellManager.GetComponent<FireSpells>().FireTorrent);
            spellSlots[2].GetComponent<Button>().onClick.AddListener(spellManager.GetComponent<FireSpells>().FlameBarrier);
        }
        else if (element == Element.Lightning)
        {
            spellSlots[0].GetComponent<Button>().onClick.AddListener(spellManager.GetComponent<LightningSpells>().StaticField);
            spellSlots[1].GetComponent<Button>().onClick.AddListener(spellManager.GetComponent<LightningSpells>().TempestSurge);
            spellSlots[2].GetComponent<Button>().onClick.AddListener(spellManager.GetComponent<LightningSpells>().ChainsOfLightning);
        }
        else if (element == Element.Aether)
        {
            spellSlots[0].GetComponent<Button>().onClick.AddListener(spellManager.GetComponent<AetherSpells>().MagneticGrasp);
            spellSlots[1].GetComponent<Button>().onClick.AddListener(spellManager.GetComponent<AetherSpells>().AethericLeap);
            spellSlots[2].GetComponent<Button>().onClick.AddListener(spellManager.GetComponent<AetherSpells>().BlackHole);
        }
    }
}
