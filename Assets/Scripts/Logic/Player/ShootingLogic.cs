using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField]
    ShootingData data = new ShootingData();

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetRateOfFire()
    {
        return data.rateOfFire;
    }

    public int GetDamage()
    {
        return data.bulletDamage;
    }
    public float GetBulletSpeed()
    {
        return data.bulletSpeed;
    }
    public void SetElement(Element element)
    {
        data.currentElement = element;
    }
    public Element GetElement()
    {
        return data.currentElement;
    }
    public void SetRateOfFire(float newRate)
    {
        data.rateOfFire = newRate;
    }

}
