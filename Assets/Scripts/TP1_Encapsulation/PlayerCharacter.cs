using System.Collections;
using UnityEngine;

namespace TP1_Encapsulation
{
    public class PlayerCharacter : MonoBehaviour
    {
        private string playerName;

        private int health;

        [SerializeField]
        private int maxHealth;

        [SerializeField]
        private float moveSpeed;

        private int gold;

        private bool isInvincible;

        private int xp;

        private int level;

        private float statMultiplier = 1f;

        #region Player name

        /// <summary>
        /// Cette méthode permet de récupérer le nom du joueur.
        /// </summary>
        /// <returns></returns>
        public string GetPlayerName()
        {
            return playerName;
        }

        /// <summary>
        /// Cette méthode permet de définir le nom du joueur.
        /// </summary>
        /// <param name="name">Le nom du joueur</param>
        public void SetPlayerName(string name)
        {
            // On vérifie que le nom du joueur n'est pas vide ou nul avant de le définir
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogWarning("Player name cannot be empty.");
                return;
            }

            playerName = name;
        }

        #endregion

        #region Health

        /// <summary>
        /// Cette méthode permet de récupérer la santé actuelle du personnage.
        /// </summary>
        /// <returns></returns>
        public int GetHealth()
        {
            return health;
        }

        public int GetMaxHealth()
        {
            return (int)(maxHealth * statMultiplier);
        }

        public bool IsDead()
        {
            return health <= 0;
        }

        /// <summary>
        /// Cette méthode permet au personnage de prendre des dégâts;
        /// </summary>
        /// <param name="amount">La quantitée de dégat à infligé</param>
        public void TakeDamage(int amount)
        {
            // On vérifie si le personnage est invincible avant de lui infliger des dégâts
            if (isInvincible) return;

            // On s'assure que les dégâts reste dans les limites minimales (0) et maximales (maxHealth)
            health -= amount;
            health = Mathf.Clamp(health, 0, GetMaxHealth());

            // On vérifie si le personnage est mort après avoir pris des dégâts
            if (health <= 0)
            {
                Debug.Log("Player is dead");
            }
        }

        /// <summary>
        /// Cette méthode permet au personnage de se soigner.
        /// </summary>
        /// <param name="amount">La quantité de soin appliqué</param>
        public void Heal(int amount)
        {
            // On vérifie que la quantité de soin est positive avant de l'appliquer
            if (amount < 0)
            {
                Debug.LogWarning("Cannot heal with a negative amount.");
                return;
            }

            // On s'assure que les soins restent dans les limites minimales (0) et maximales (maxHealth)
            health += amount;
            health = Mathf.Clamp(health, 0, GetMaxHealth());
        }

        #endregion

        #region Move speed

        /// <summary>
        /// Cette méthode permet de récupérer la vitesse de déplacement du personnage.
        /// </summary>
        /// <returns></returns>
        public float GetMoveSpeed()
        {
            return moveSpeed * statMultiplier;
        }

        /// <summary>
        /// Cette méthode permet de définir la vitesse de déplacement du personnage.
        /// </summary>
        /// <param name="speed"></param>
        public void SetMoveSpeed(float speed)
        {
            // On s'assure que la vitesse de déplacement reste dans une plage raisonnable
            speed = Mathf.Clamp(speed, 0f, 100f);

            moveSpeed = speed;
        }

        #endregion

        #region Gold

        /// <summary>
        /// Cette méthode permet de récupérer la quantité d'or que possède le personnage.
        /// </summary>
        /// <returns></returns>
        public int GetGold()
        {
            return gold;
        }

        /// <summary>
        /// Cette méthode permet d'ajouter de l'or au personnage.
        /// </summary>
        /// <param name="amount"></param>
        public void AddGold(int amount)
        {
            // On s'assure que la quantité d'or ajoutée est positive
            if (amount < 0)
            {
                Debug.LogWarning("Cannot add negative gold.");
                return;
            }
            gold += amount;
        }

        /// <summary>
        /// Cette méthode permet de retirer de l'or au personnage.
        /// </summary>
        /// <param name="amount"></param>
        public void RemoveGold(int amount)
        {
            // On s'assure que la quantité d'or retirée est positive
            if (amount < 0)
            {
                Debug.LogWarning("Cannot remove negative gold.");
                return;
            }

            // On vérifie que le personnage ne peut pas perdre plus d'or qu'il n'en possède
            if (amount > gold)
            {
                Debug.LogWarning("Cannot remove more gold than the player currently has.");
                return;
            }

            gold -= amount;
        }

        #endregion

        #region Invincibility

        /// <summary>
        /// Cette méthode permet de vérifier si le personnage est actuellement invincible.
        /// </summary>
        /// <returns></returns>
        public bool IsInvincible()
        {
            return isInvincible;
        }

        /// <summary>
        /// Cette méthode permet d'activer l'invincibilité du personnage pendant une durée spécifiée.
        /// </summary>
        /// <param name="duration">La durée d'activation de l'invincibilité</param>
        public void ActivateInvincibility(float duration)
        {
            if (!isInvincible)
                StartCoroutine(ActiveInvincibilityRoutine(duration));
        }

        /// <summary>
        /// Cette méthode permet d'activer l'invincibilité du personnage pendant une durée spécifiée.
        /// </summary>
        /// <param name="duration">La durée d'invincibilité</param>
        /// <returns></returns>
        private IEnumerator ActiveInvincibilityRoutine(float duration)
        {
            isInvincible = true;
            yield return new WaitForSeconds(duration);
            isInvincible = false;
        }

        #endregion

        #region XP

        /// <summary>
        /// Cette méthode permet de récupérer la quantité d'expérience que possède le personnage.
        /// </summary>
        /// <returns></returns>
        public int GetXP()
        {
            return xp;
        }

        /// <summary>
        /// Cette méthode permet d'ajouter de l'expérience au personnage et de vérifier s'il a atteint le seuil pour monter de niveau.
        /// </summary>
        /// <param name="amount"></param>
        public void AddXP(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot add negative XP.");
                return;
            }
            xp += amount;

            CheckLevelUp();
        }

        /// <summary>
        /// Cette méthode vérifie si le personnage a atteint le seuil d'expérience nécessaire pour monter de niveau et effectue la montée de niveau si nécessaire.
        /// </summary>
        private void CheckLevelUp()
        {
            if (xp >= level * 100) // Simple calcul pour déterminer la de montée de niveau.
            {
                level++;
                statMultiplier += 0.1f;

                Debug.Log("Level Up! Current Level: " + level);
            }
        }

        /// <summary>
        /// Cette méthode permet de récupérer le niveau actuel du personnage.
        /// </summary>
        /// <returns></returns>
        public int GetLevel()
        {
            return level;
        }

        #endregion
    }
}
