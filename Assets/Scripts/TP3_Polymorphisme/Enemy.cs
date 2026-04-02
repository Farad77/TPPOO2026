using UnityEngine;

namespace TP3_Polymorphisme
{
    // Version simplifiée de l'ennemi du TP2 pour le TP3
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected int health;
        [SerializeField] protected int damage;
        
        public virtual void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Die();
            }
        }
        
        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}