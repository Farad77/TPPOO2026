using System.Collections;
using UnityEngine;

namespace TP1_Encapsulation.Correction
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private string playerName;
        [SerializeField] private int health;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private int gold;
        [SerializeField] private float mana = 100f; 
        [SerializeField] private float maxMana = 100f; 
        private bool isInvincible;

        // Propriétés encapsulées avec validation
        public string PlayerName { get { return playerName; } }

        //Health=-100
        public int Health
        {
            get { return health; }
             set { health = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
             set { maxHealth = Mathf.Max(1, value); }
        }

        public float MoveSpeed
        {
            get { return moveSpeed; }
             set { moveSpeed = Mathf.Clamp(value, 0.5f, 20f); }
        }

        public int Gold
        {
            get { return gold; }
             set { gold = Mathf.Max(0, value); }
        }

        public bool IsInvincible
        {
            get { return isInvincible; }
             set { isInvincible = value; }
        }

       
        public float Mana
        {
            get { return mana; }
             set { mana = Mathf.Clamp(value, 0, maxMana); }
        }

        public float MaxMana
        {
            get { return maxMana; }
             set { maxMana = Mathf.Max(1, value); }
        }

        private void Start()
        {
            // Initialisation avec validation
            Health = maxHealth;
            Mana = maxMana;
        }

        void Update()
        {
            if (Health <= 0)
            {
                Die();
            }

           

           
            if (Mana < MaxMana)
            {
                Mana += 0.1f * Time.deltaTime;
            }
        }

        // Méthodes publiques avec logique de validation
        public void TakeDamage(int damage)
        {
            //if (isInvincible==true) return;
            if (isInvincible) return;
            if (damage > 0)
                Health -= damage;
           
        }

        public void Heal(int amount)
        {
            if (amount > 0)
                Health += amount;
        }

        public void GainGold(int amount)
        {
            if (amount > 0)
                Gold += amount;
        }

        public bool SpendGold(int amount)
        {
            if (Gold >= amount && amount > 0)
            {
                Gold -= amount;
                return true;
            }
            return false;
        }

      
        public bool SpendMana(float amount)
        {
            if (Mana >= amount && amount > 0)
            {
                Mana -= amount;
                return true;
            }
            return false;
        }

        public void ActivateInvincibility(float duration)
        {
            StartCoroutine(InvincibilityRoutine(duration));
        }

        public void IncreaseMaxHealth(int amount)
        {
            if (amount > 0)
            {
                MaxHealth += amount;
                // Mettre à jour la santé proportionnellement à l'augmentation
                float healthRatio = (float)Health / (MaxHealth - amount);
                Health = Mathf.RoundToInt(healthRatio * MaxHealth);
            }
        }

        public void SetMoveSpeed(float newSpeed)
        {
            MoveSpeed = newSpeed; // La validation est faite dans la propriété
        }

        // Méthodes privées
        private IEnumerator InvincibilityRoutine(float duration)
        {
            IsInvincible = true;
            yield return new WaitForSeconds(duration);
            IsInvincible = false;
        }

        private void Die()
        {
            Debug.Log($"Player {PlayerName} is dead!");
            // Logique de mort ici
        }
    }

}