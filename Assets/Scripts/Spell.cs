using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "ScriptableObjects/SpellData", order = 1)]
public class Spell : ScriptableObject
{
    public string spellName;
    public Element spellElement;
    public Sprite spellSprite;
    public int spellManaCost;
    public int spellDamage;
    public int spellAreaDamage;

    public float spellCooldown;
    public float spellDuration;

}


public enum Element
{
    None,
    Fire,
    Lightning,
    Aether

};
