using UnityEngine;

namespace TP3_Polymorphisme.Correction
{
    // Version simplifiée de l'ennemi du TP2 pour la correction du TP3
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected int health;
        [SerializeField] protected int damage;
        
        public virtual void TakeDamage(int amount)
        {
            Debug.Log(name + " tanking " + amount + "dmg");
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