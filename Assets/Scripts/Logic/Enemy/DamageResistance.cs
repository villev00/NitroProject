using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageResistance", menuName = "ScriptableObjects/DamageResistance", order = 1)]
public class DamageResistance : ScriptableObject
{
    [System.Serializable]
   public struct DamageResistanceData
    {
      public float fireResistance;
      public float lightningResistance;
      public float aetherResistance;
   }

    public List<DamageResistanceData> resistanceData = new List<DamageResistanceData>();

   
    public float GetResistance(Element element)
    {
        switch (element)
        {
            case Element.Fire:
                return resistanceData[0].fireResistance;
            case Element.Lightning:
                return resistanceData[0].lightningResistance;
            case Element.Aether:
                return resistanceData[0].aetherResistance;
            default:
                return 0;
        }
    }
   
    public float CalculateDamageWithResistance(float damage, Element element)
    {
        float resistance = GetResistance(element);
        float damageAfterResistance = damage - (damage * resistance);
        return damageAfterResistance;
    }

}
