using System;
//using UnityEngine;

namespace data
{
    [Serializable]

    public class PlayerData
    {
        public int health;
        public  int maxHealth;
        public int lives = 5;
        public int shield = 0;
        public float moveSpeed = 8;
        public float jumpForce = 30;

        public int mana;
        public int maxMana;

    }
    
}




