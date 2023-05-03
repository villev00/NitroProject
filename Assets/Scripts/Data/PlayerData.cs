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
        public float moveSpeed = 6;
        public float jumpForce = 1.9f;
        public int mana;
        public int maxMana;
    }
}




