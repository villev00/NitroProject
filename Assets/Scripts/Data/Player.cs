using System;

namespace data
{
    [Serializable]

    public class Player
    {
        public int health;
        public  int maxHealth;
        public int lives = 5;
        public int shield = 0;
        public float moveSpeed = 8;
        public float jumpForce = 2;
    }
    
}




