using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class FloatingCombatText : MonoBehaviour
{
    [SerializeField]
    Transform damageText;
    [SerializeField]
    GameObject pfDamagePopUp;

    TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    
    public static FloatingCombatText Create(Vector3 position, float damageAmount)
    {
        Transform damagePopUpTransform = Instantiate(FloatingCombatTextManager.i.pfDamagePopUp, position, Quaternion.identity);
        FloatingCombatText damagePopUp = damagePopUpTransform.GetComponent<FloatingCombatText>();
        damagePopUp.SetUp(damageAmount);
        return damagePopUp;
    }
    
    void SetUp(float damage)
    {
        textMesh.SetText(damage.ToString());
    }
}
