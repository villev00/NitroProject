using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    Transform bulletSpawn;
    ShootingLogic sLogic;
    ShootingData sData = new ShootingData();
    public float rateOfFire;
    public float bulletSpeed;
    bool readyToShoot;
    //[SerializeField]
    Camera playerCamera;
    [SerializeField]
    GameObject bullet;

    public UnityAction statChange;
    


    private void Awake()
    {
        sLogic = GetComponent<ShootingLogic>();
        playerCamera = GetComponentInChildren<Camera>(); // Not like this, spagetti ratkasu
        bulletSpawn = GameObject.Find("SpellSpawn").GetComponent<Transform>(); // Not optimal
        statChange += FetchData;
    }

    private void Start()
    {
        FetchData();
        sLogic.SetElement(Element.Fire);
        readyToShoot = true;
    }

    void Update()
    {
        UserInput();
    }

    void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        
        readyToShoot = false;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit, 999, 1))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }
        Vector3 direction = targetPoint - bulletSpawn.position;
        GameObject currentBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        currentBullet.transform.forward = direction.normalized;
        currentBullet.GetComponent<Bullet>().element = sLogic.GetElement();
        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(ResetShot());
       
    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(1 / rateOfFire);
        readyToShoot = true;
    }

    void FetchData()
    {
        rateOfFire = sLogic.GetRateOfFire();
        bulletSpeed = sLogic.GetBulletSpeed();
       
    }
}
