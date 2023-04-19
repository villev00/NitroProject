using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SpellData", menuName = "ScriptableObjects/SpellData", order = 1)]
public class Spell : ScriptableObject
{
    public string spellName;
    [TextArea(5,5)]
    public string spellInfo;
    public Element spellElement;
    public GameObject spellPrefab;
    public Sprite spellSprite;

    [Header("Spell stats")]
    public int spellManaCost;
    public int spellDamage;
    public int spellAreaDamage;

    public float spellCooldown;
    public bool isSpellOnCooldown;
    public float spellDuration;

    public GameObject spellUIPrefab;
    public float cooldownRemaining;


    private void Awake()
    {
        isSpellOnCooldown = false;
        cooldownRemaining = 0;
    }
    public IEnumerator CountSpellCooldown()
    {
        isSpellOnCooldown = true;
        cooldownRemaining = spellCooldown;
        while (isSpellOnCooldown)
        {
            yield return new WaitForSeconds(1f);
            cooldownRemaining -= 1f;
            if(cooldownRemaining <= 0)
                isSpellOnCooldown = false;
        }
    }
}


public enum Element
{
    None,
    Fire,
    Lightning,
    Aether

};
