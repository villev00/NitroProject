using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "ScriptableObjects/SpellData", order = 1)]
public class Spell : ScriptableObject
{
    public string spellName;
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

  
    public IEnumerator SpellCooldown()
    {
        yield return new WaitForSeconds(spellCooldown);
        isSpellOnCooldown = false;
        Debug.Log(spellName+" ready");
    }
 
}


public enum Element
{
    None,
    Fire,
    Lightning,
    Aether

};
