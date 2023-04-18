using System;
using System.Collections.Generic;
using data;
using Photon.Pun;
using UnityEngine;

public class PlayerLogic : MonoBehaviour

{
    PhotonView pv;
    public GameObject flameBarrier;
    [SerializeField]
    PlayerData data = new PlayerData();
    PlayerUI playerUI;

    [SerializeField]
    GameObject[] allPlayers = new GameObject[2];

    public GameObject otherPlayer;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            playerUI = GameObject.Find("Managers").transform.GetChild(1).GetComponent<PlayerUI>();
            playerUI.ChangeLives(5);
        }
    }
    private void Start()
    {
        Invoke("FindPlayers", 2);
      
    }
    void FindPlayers()
    {
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in allPlayers)
        {
            if (!player.GetComponent<PhotonView>().IsMine)
            {
                otherPlayer = player;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        return;
        if (flameBarrier != null)
            {
                data.shield -= damage;
                if (data.shield < 0)
                {
                    data.health += data.shield;
                    data.shield = 0;
                    Destroy(flameBarrier);
                }
            }
            else if (pv.IsMine) 
            {
                data.health -= damage;
                playerUI.ChangeHealthSliderValue(-damage);
            }
        
        if (data.health <= 0)
        {
            Die();
        }
    }
 
   
    public void SetShieldValue(int value)
    {
        data.shield = value;
    }
    public void Heal(int heal)
    {
        data.health += heal;
        if (pv.IsMine)
            playerUI.ChangeHealthSliderValue(heal);
        if (data.health > data.maxHealth)
        {
            data.health = data.maxHealth;
        }
    }
    public int GetMana()
    {
        return data.mana;
    }
    public int GetHealth()
    {
        return data.health;
    }
    public int GetMaxHealth()
    {
        return data.maxHealth;
    }
    public void LoseMana(int amount)
    {
        if (pv.IsMine)
        {
            data.mana -= amount;
            playerUI.ChangeManaSliderValue(-amount);
            if (data.mana > data.maxMana)
            {
                data.mana = data.maxMana;
            }
            if (data.mana < 0)
            {
                data.mana = 0;
            } 
        }
        otherPlayer.GetComponent<PhotonView>().RPC("RPC_GainMana", RpcTarget.All, amount);    
    }


    [PunRPC]
    void RPC_GainMana(int amount)
    {
        data.mana += amount;
        if (pv.IsMine)
            playerUI.ChangeManaSliderValue(amount);
        if (data.mana > data.maxMana)
        {
            data.mana = data.maxMana;
        }
    }

    public float GetSpeed()
    {
        return data.moveSpeed;
    }
    public void SetSpeed(float speed)
    {
        data.moveSpeed = speed;
    }
    public float GetJumpForce()
    {
        return data.jumpForce;
    }
    public IEnumerable<WaitForSeconds> Tickdamage(int damage, int tickrate , int duration) 
    {
        for (int i = 0; i < duration; i++)
        {
            TakeDamage(damage);
            Debug.Log("Player took " + damage + " damage");
               yield return new WaitForSeconds(tickrate);
        }  
    }
    
    public IEnumerable<WaitForSeconds>  Healtick(int heal, int tickrate , int duration) 
    {
        
        for (int i = 0; i < duration; i++)
        {
            Heal(heal);
            Debug.Log("Player healed " + heal + " health");
            yield return new WaitForSeconds(tickrate);
        }  
    }

    public void Die()
    {
      Debug.Log("Player died");
      otherPlayer.GetComponent<PhotonView>().RPC("KillFriend", RpcTarget.All);
      LoseLife();
    }
   
    [PunRPC]
    public void KillFriend()
    {
        if (pv.IsMine) 
        {
            data.lives -= 1;

            playerUI.ChangeLives(1);
            if (data.lives <= 0)
            {
                GameOver();
            }
        }
            
    }
    public void LoseLife()
    {
        data.lives -= 1;
        if(pv.IsMine)
            playerUI.ChangeLives(1);
        if (data.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Heal(data.maxHealth);
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TestDamage(10);
        }
      
    }


    public void GameOver()
    {
        Time.timeScale = 0;
        playerUI.GameOverPanel();
    }

    public void TestDamage(int damage)
    {
       
        if (flameBarrier != null)
        {
            data.shield -= damage;
            if (data.shield < 0)
            {
                data.health += data.shield;
                data.shield = 0;
                Destroy(flameBarrier);
            }
        }
        else if (pv.IsMine)
        {
            data.health -= damage;
            playerUI.ChangeHealthSliderValue(-damage);
        }

        if (data.health <= 0)
        {
            Die();
        }
    }

}