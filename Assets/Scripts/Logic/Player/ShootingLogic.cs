using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField]
    ShootingData data = new ShootingData();
    void Start()
    {
        
    }

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
    public int GetHeadshotDamage()
    {
        return data.headshotDamage;
    }
    public float GetBulletSpeed()
    {
        return data.bulletSpeed;
    }
    public void SetElement(Element element)
    {
        data.currentElement = element;
    }
    public void SetRateOfFire(float newRate)
    {
        data.rateOfFire = newRate;
    }
}
