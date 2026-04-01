
using System.Collections;
using UnityEngine;

namespace TP1_Encapsulation
{
    public class PlayerCharacter : MonoBehaviour
    {
        // Toutes les données sont publiques et peuvent être modifiées n'importe où
        private string playerName;
        [SerializeField] private int health = 0;
        private int maxHealth = 100;
        private int healthRegen = 1;
        [SerializeField] private float moveSpeed = 0f;
        [SerializeField] private int gold = 0;
        private int maxGold = 25;
        private bool isInvincible = false;
        private int currentXp;
        private int xpNeeded;
        private int currentLvl = 1;

        private void Start()
        {
            health = maxHealth;
        }

        void Update()
        {
            if (health > maxHealth / 2)
            {
                StopCoroutine(HealthRegen());
            }
            else if (health <= maxHealth / 2)
            {
                StartCoroutine(HealthRegen());
            }
            else if (health <= 0)
            {
                Debug.Log("Player is dead");
                StopCoroutine(HealthRegen());
            }

            // La vitesse peut être modifiée à n'importe quelle valeur
            // Le personnage peut avoir une vitesse négative car rien ne l'empêche

            if (moveSpeed < 0)
            {
                moveSpeed = 0;
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); 
        }

        IEnumerator HealthRegen()
        {
            yield return new WaitForSeconds(1);
            GetHealth(healthRegen);
        }

        public void GetXp(int amount)
        {
            currentXp += amount;
            if (currentXp > xpNeeded)
            {
                currentXp -= xpNeeded;
                LevelUp();
            }
        }

        public void LevelUp()
        {
            currentLvl++;
        }

        public void GainGold(int amount)
        {
            gold += amount;
            gold = Mathf.Clamp(gold, 0, maxGold);
        }

        public void GiveGold(int amount)
        {
            if (gold >= amount)
            {
                gold -= amount;
                gold = Mathf.Clamp(gold, 0, maxGold);
            }
        }


        public void TakeDamage(int amount)
        {
            health -= amount;
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        public void GetHealth(int amount)
        {
            health += amount;
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        public int GetCurrentHealth()
        {
            return health;
        }

        public float GetSpeed()
        {
            return moveSpeed;
        }

        public string GetName()
        {
            return name;
        }

        public int GetGold()
        {
            return gold;
        }

        public bool IsPlayerInvincible()
        {
            return isInvincible;
        }
    }
}