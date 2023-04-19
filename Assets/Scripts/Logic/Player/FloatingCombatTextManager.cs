using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCombatTextManager : MonoBehaviour
{
    private static FloatingCombatTextManager _i;

    public static FloatingCombatTextManager i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<FloatingCombatTextManager>("FloatingCombatTextManager"));
            return _i;
        }
    }

    public Transform pfDamagePopUp;
}
