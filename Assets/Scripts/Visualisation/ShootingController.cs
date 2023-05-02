using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    int bulletAmount;
    bool readyToShoot;
    [SerializeField] float reloadTime;
    //[SerializeField]
    Camera playerCamera;
    [SerializeField]
    GameObject bullet;
    GameObject currentBullet;


    public UnityAction statChange;
    PlayerUI playerUI;

    PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) return;
        sLogic = GetComponent<ShootingLogic>();
        playerUI = GameObject.Find("UIManager").GetComponent<PlayerUI>();
        playerCamera = Camera.main.GetComponent<Camera>();
        //playerCamera = GameObject.Find("Camera").GetComponent<Camera>();
        //playerCamera = GetComponentInChildren<Camera>(); // Not like this, spagetti ratkasu
        //bulletSpawn = GameObject.Find("SpellSpawn").GetComponent<Transform>(); // Not optimal
        bulletSpawn = Camera.main.transform.GetChild(0).GetChild(0).transform;
        statChange += FetchData;
       
    }

    private void Start()
    {
        if (!pv.IsMine) return;
        FetchData();
        sLogic.SetElement(Element.Fire);
        readyToShoot = true;
        playerUI.UpdateAmmoAmount(bulletAmount.ToString());
    }

    public Transform GetSpellSpawn()
    {
        return bulletSpawn;
    }
    void FixedUpdate()
    {
       
        UserInput();
    }

    void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot && Cursor.lockState == CursorLockMode.Locked && bulletAmount>0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        bulletAmount--;
        playerUI.UpdateAmmoAmount(bulletAmount.ToString());
        if (bulletAmount == 0) StartCoroutine(ReloadStaff());
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
        // GameObject currentBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);

        switch (sLogic.GetElement())
        {
            case Element.Fire:
                currentBullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Fire Bullet"), bulletSpawn.position, Quaternion.identity);
                break;

            case Element.Aether:
                currentBullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Aether Bullet"), bulletSpawn.position, Quaternion.identity);
                break;

            case Element.Lightning:
                currentBullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Lightning Bullet"), bulletSpawn.position, Quaternion.identity);
                break;

            default:
                break;
        }

        currentBullet.transform.forward = direction.normalized;
        currentBullet.GetComponent<Bullet>().element = sLogic.GetElement();
        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(ResetShot());
       
    }
    IEnumerator ReloadStaff()
    {
        float waitTime = reloadTime;
        playerUI.UpdateAmmoAmount(waitTime.ToString());
        while (waitTime != 0)
        {
            yield return new WaitForSeconds(1);
            waitTime -= 1;
            playerUI.UpdateAmmoAmount(waitTime.ToString());
        }

        bulletAmount = sLogic.GetBulletAmount();
        playerUI.UpdateAmmoAmount(bulletAmount.ToString());
    }
    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(1 / rateOfFire);
        readyToShoot = true;
        playerUI.UpdateAmmoAmount(bulletAmount.ToString());
    }

    void FetchData()
    {
        rateOfFire = sLogic.GetRateOfFire();
        bulletSpeed = sLogic.GetBulletSpeed();
        bulletAmount = sLogic.GetBulletAmount();
        reloadTime = sLogic.GetReloadTime();
       
    }
}
