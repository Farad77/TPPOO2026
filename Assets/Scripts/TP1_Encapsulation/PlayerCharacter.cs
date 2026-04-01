using UnityEngine;

namespace TP1_Encapsulation
{
    public class PlayerCharacter : MonoBehaviour
    {
        [Header("Player Data")]
        [Header("Player Name")]
        [SerializeField] private string playerName; // player's name ; Not meant for direct access.

        [Header("Player Life System")] // baseHealth is the health at the start of the game. max health is the maximum reachable health, and health is the current health, not meant to be accessed directly

        [SerializeField] private int baseHealth;
        [SerializeField] private int maxHealth;

        private int health;

        // If the player is invincible, doesn't take damage.

        [SerializeField] private bool baseInvincible;
        private bool isInvincible;

        [Header("Movement")] // baseSpeed is the speed at the start, maxSpeed is the maximum speed reachable & moveSpeed is the current speed and not to be modified directly
        
        [SerializeField] private float baseSpeed;
        [SerializeField] private float maxSpeed;

        private float moveSpeed;

        [Header("Gold")] // gold is the amount of gold the player has and is not meant to be exposed. baseGold is the gold amount at start

        [SerializeField] private int baseGold;
        [SerializeField] private int maxGold;
        private int gold;


        /*
        Note : The Update was removed for debugging reasons as it caused continuous movement and was unpractical to be used while testing
        
        private void Update()
        {
            transform.Translate(Vector3.forward * GetSpeed() * Time.deltaTime);
        }
        */


        private void Start()
        {
            ResetVariables();
        }


        private void OnValidate() // If changed in the editor in runtime
        {
            ResetVariables();
        }


        private void ResetVariables() // Set the variables to their base state
        {
            SetHealth(baseHealth);
            SetGold(baseGold);
            SetInvincibility(baseInvincible);
            SetSpeed(baseSpeed);
        }


        // GETTER & SETTER for the playerName variable so a game manager can set it and can access the player name in case we need to check for multiple


        public string GetName() => playerName;


        public void SetName(string name) => playerName = name;


        // GETTER & SETTER for the move speed to cap it between [0 - max speed]


        public float GetSpeed() => moveSpeed;
        

        public float GetMaxSpeed() => maxSpeed;


        public void SetSpeed(float newSpeed)
        {
            float max = GetMaxSpeed();
            if (max == 0) // If maxSpeed is 0 new Speed will obligatory be 0
            {
                moveSpeed = 0;
                return;
            }
            moveSpeed = (newSpeed >= max) ? max : (newSpeed <= 0) ? 0 : newSpeed;  // Checks if it is in between 0 & maxSpeed
            return;
        }


        // Health functions with a GETTER & SETTER that caps health between [0 - max health] and checks if the player is dead
        
        
        public int GetHealth() => health;

        public int GetMaxHealth() => maxHealth;


        public void SetHealth(int newHealth)
        {
            int max = GetMaxHealth();
            health = (newHealth <= 0) ? 0 : (newHealth >= max) ? max : newHealth;
            if (IsDead()) Death();
        }


        public bool IsDead()
        {
            return (!isInvincible && GetHealth() == 0);
        }


        public void Death()
        {
            Debug.Log("Player is dead");
        }
        
        
        // GETTER & SETTER for the gold amount


        public int GetGold() => gold;


        public void SetGold(int newGold)
        {
            gold = (newGold <= 0) ? 0 : (newGold >= maxGold) ? maxGold : newGold;
        }


        // GETTER & SETTER for invincibility
        public bool IsInvincible() => isInvincible;


        public void SetInvincibility(bool newState) => isInvincible = newState;


        // Other custom functions for QoL

        public void GainGold(int amount)
        {
            if (amount <= 0) return;
            SetGold(amount + GetGold());
        }


        public void SpendGold(int amount)
        {
            if (GetGold() - amount < 0 | amount <= 0) return;
            SetGold(GetGold() - amount);
        }


        public void TakeDamage(int amount)
        {
            if (amount <= 0) return;
            SetHealth(GetHealth() - amount);
        }


        public void Heal(int amount)
        {
            if (amount <= 0) return;
            SetHealth(GetHealth() + amount);
        }
    }
}