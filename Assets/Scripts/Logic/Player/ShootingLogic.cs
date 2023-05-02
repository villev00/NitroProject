using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField]
    ShootingData data = new ShootingData();

    public int GetBulletAmount()
    {
        return data.bulletAmount;
    }
    public void SetBulletAmount(int amount)
    {
        data.bulletAmount = amount;
    }
    public float GetRateOfFire()
    {
        return data.rateOfFire;
    }
    public float GetReloadTime()
    {
        return data.reloadTime;
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
