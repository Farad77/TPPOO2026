using System;
using System.Collections;
using UnityEngine;

namespace TP1_Encapsulation
{
    public class PlayerCharacter : MonoBehaviour
    {

        [SerializeField]
        protected string playerName;

        [Header("Health Settings")]

        [SerializeField]
        private int health;
        [SerializeField]
        private int maxHealth;

        [Header("Speed Settings")]

        [Range(0f, maxSpeed)]
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private const float maxSpeed = 100f;

        [Header("XP Settings && Level")]
        private int xp = 0;
        private int xpToNextLevel = 100;
        private int level = 1;

        private float statMultiplier = 1f;


        [Header("Gold Settings")]
        [SerializeField]
        private int gold;


        [Header("Invincibility Settings")]

        [SerializeField]
        private bool isInvincible;
        [SerializeField]
        private float invincibilityDuration = 5f;

        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Health { get => health; set => health = Mathf.Clamp(value, 0, MaxHealth); }

        public bool IsDead { get => health <= 0;}


        void Update()
        {

            if (Health <= 0)
            {
                Debug.Log("Player is dead");
            }

            // La vitesse peut être modifiée uniquement via les méthodes setMoveSpeed, qui limitent
            // la valeur entre 0 et 100
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }


        #region PlayerName


        /// <summary>
        /// Permet d'obtenir le nom du personnage. Le nom peut être défini uniquement via le setter
        /// </summary>
        /// <returns></returns>
        public string getPlayerName()
        {
            return playerName;
        }


        /// <summary>
        /// Permet de définir le nom du personnage. Le nom peut être défini uniquement via ce setter pour éviter des modifications non contrôlées.
        /// </summary>
        /// <param name="newName"></param>
        public void setPlayerName(string newName)
        {
            playerName = newName;
        }

        #endregion

        #region Speed

        /// <summary>
        /// Permet d'obtenir la vitesse de déplacement du personnage. La vitesse est limitée entre 0 et 100 
        /// via la méthode setMoveSpeed.
        /// </summary>
        /// <returns></returns>
        public float getMoveSpeed()
        {
            return moveSpeed * statMultiplier;
        }


        /// <summary>
        /// Permet de définir la vitesse de déplacement du personnage. La vitesse est limitée entre 0 et maxSpeed 
        /// pour éviter des valeurs non réalistes ou négatives.
        /// </summary>
        /// <param name="newSpeed"></param>
        public void setMoveSpeed(float newSpeed)
        {
            if (newSpeed <= 0)
                moveSpeed = 0;

            else if (newSpeed > maxSpeed)
                moveSpeed = maxSpeed;

            else
                moveSpeed = newSpeed;
        }

        #endregion

        #region Health
        /// <summary>
        /// Permet d'obtenir la santé actuelle du personnage. La santé est limitée entre 0 et maxHealth via 
        /// les méthodes TakeDamage et Heal.
        /// </summary>
        /// <returns></returns>
        public float getHealth()
        {
            return Health;
        }

        /// <summary>
        /// Permet de définir la santé du personnage. La santé est limitée entre 0 et maxHealth pour éviter des valeurs non réalistes ou négatives.
        /// La santé ne peut pas être modifiée directement, mais uniquement via les méthodes TakeDamage et Heal, qui appliquent les règles de santé.
        /// </summary>
        /// <param name="newHealth"></param>
        /// <returns></returns>
        public float setHealth(int newHealth)
        {
            if (newHealth < 0)
                Health = 0;
            else if (newHealth > MaxHealth)
                Health = MaxHealth;
            else
                Health = newHealth;
            return Health;
        }

        /// <summary>
        /// Permet de soigner le personnage en augmentant sa santé. La santé ne peut pas dépasser maxHealth et 
        /// ne peut pas être soignée si elle est déjà à 0 ou à maxHealth.
        /// </summary>
        /// <param name="amount"></param>
        public void Heal(int amount)
        {
            if (Health >= MaxHealth || Health <= 0)
            {
                return;
            }

            int temphealth = Health + amount;
            setHealth(temphealth);
        }

        /// <summary>
        /// Permet d'infliger des dégâts au personnage en réduisant sa santé. La santé ne peut pas descendre en dessous de 0 
        /// et ne peut pas être endommagée si elle est déjà à 0 ou si le personnage est invincible.
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(int amount)
        {
            if (Health <= 0 || isInvincible)
            {
                return;
            }

            int temphealth = Health + amount;
            setHealth(temphealth);

        }


        #endregion

        #region Gold

        /// <summary>
        /// Permet d'obtenir la quantité d'or du personnage. L'or ne peut être modifié que via les méthodes GainGold et SpendGold.
        /// </summary>
        /// <returns></returns>
        public float getGold()
        {
            return gold;
        }

        /// <summary>
        /// Permet de définir la quantité d'or du personnage. L'or ne peut être négatif, 
        /// et ne peut être modifié que via les méthodes GainGold et SpendGold.
        /// </summary>
        /// <param name="amount"></param>
        public void setGold(int amount)
        {
            gold = amount;
        }

        /// <summary>
        /// Permet d'augmenter la quantité d'or du personnage. L'or ne peut pas être négatif, 
        /// et ne peut être modifié que via les méthodes GainGold et SpendGold.
        /// </summary>
        /// <param name="amount"></param>
        public void GainGold(int amount)
        {
            if (amount < 0)
            {
                Debug.Log("Cannot gain a negative amount of gold!");
                return;
            }
            int tempGold = gold + amount;
            setHealth(tempGold);
        }


        /// <summary>
        /// Permet de réduire la quantité d'or du personnage. L'or ne peut pas être négatif. 
        /// Le personnage ne peut pas dépenser plus d'or qu'il n'en possède.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool SpendGold(int amount)
        {
            if (amount > gold)
            {
                Debug.Log("Not enough gold to spend!");
                return false;
            }
            int tempGold = gold - amount;
            setHealth(tempGold);

            return true;
        }

        #endregion

        #region XP and Level

        public int getXP()
        {
            return xp;
        }

        public void setXP(int newXP)
        {
            if (newXP < 0)
                xp = 0;
            else if (newXP > xpToNextLevel)
            {
                xp = xpToNextLevel - xp; // Reste de l'XP après le niveau 
                xpToNextLevel +=100; // Augmente la quantité d'XP nécessaire pour le prochain
                AddLevel(1);
            }
            else
                xp = newXP;
        }

        public void AddXP(int amount)
        {
            if (amount < 0)
            {
                Debug.Log("Cannot add a negative amount of XP!");
                return;
            }
            int tempXP = xp + amount;
            setXP(tempXP);
        }

        public int getLevel()
        {
            return level;
        }

        public void setLevel(int newLevel)
        {
            if (newLevel < 0)
                {
                Debug.Log("Level cannot be negative!");
                return;
            }
            level = newLevel;
        }

        public void AddLevel(int amount)
        {
            if (amount < 0)
            {
                Debug.Log("Cannot add a negative amount of levels!");
                return;
            }
            statMultiplier += 0.1f; // Augmente le multiplicateur de stats à chaque niveau
            int tempLevel = level + amount;
            setLevel(tempLevel);
        }

        #endregion

        #region Invincibility


        public bool isPlayerInvincible()
        {
            return isInvincible;
        }

        /// <summary>
        /// Permet de définir l'invincibilité du personnage. Lorsque le personnage devient invincible, 
        /// il ne peut pas subir de dégâts pendant une durée définie par invincibilityDuration.
        /// </summary>
        /// <param name="invincible"></param>
        public void SetInvincibility(bool invincible)
        {
            if (invincible)
            {
                Debug.Log("Player is now invincible!");
                StartCoroutine(InvincibilityDuration(invincibilityDuration));
            }
            else
            {
                Debug.Log("Player is no longer invincible.");
                isInvincible = false;
            }
        }


        /// <summary>
        /// Coroutine qui gère la durée de l'invincibilité du personnage. Pendant la durée d'invincibilité, le personnage ne peut pas subir de dégâts.
        /// </summary>
        /// <param name="invincibilityDuration"></param>
        /// <returns></returns>
        private IEnumerator InvincibilityDuration(float invincibilityDuration)
        {
            isInvincible = true;
            yield return new WaitForSeconds(invincibilityDuration);
            isInvincible = false;
            Debug.Log("Player's invincibility has worn off.");
        }

        #endregion
    }
}