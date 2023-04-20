using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpAsset : MonoBehaviour
{
    private static DamagePopUpAsset _i;

    public static DamagePopUpAsset i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<DamagePopUpAsset>("DamagePopUpAsset"));
            return _i;
        }
    }

    public GameObject pfDamagePopUp;
}
