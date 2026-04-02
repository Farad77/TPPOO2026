using UnityEngine;
using TP1_Encapsulation.Correction;

namespace TP2_Heritage.Correction
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected int health;
        [SerializeField] protected int damage;
        [SerializeField] protected float speed;
        [SerializeField] protected float detectionRange;
        
        protected Transform player;
        
        protected virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            InitializeStats();
        }
        
        // Méthode abstraite que chaque ennemi doit implémenter
        protected abstract void InitializeStats();
        
        protected virtual void Update()
        {
            if (Vector3.Distance(transform.position, player.position) < detectionRange)
            {
                MoveTowardsPlayer();
            }
        }
        
        protected virtual void MoveTowardsPlayer()
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.position=new Vector3(transform.position.x, Mathf.Max(player.position.y,2), transform.position.z);
        }
        
        public virtual void TakeDamage(int amount)
        {
            Debug.Log($"{gameObject.name} takes {amount} damage.");
            health -= amount;
            OnDamageReceived(amount);
            
            if (health <= 0)
            {
                Die();
            }
        }
        
        // Hook qui permet aux sous-classes de réagir aux dégâts
        protected virtual void OnDamageReceived(int amount)
        {
            // Comportement de base - peut être surchargé
            Debug.Log($"{gameObject.name} a reçu {amount} dégâts. Santé restante : {health}");
        }
        
        protected virtual void Die()
        {
            Destroy(gameObject);
        }
        
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                DealDamageToPlayer(collision.gameObject.GetComponent<TP1_Encapsulation.Correction.PlayerCharacter>());
            }
        }
        
        protected virtual void DealDamageToPlayer(TP1_Encapsulation.Correction.PlayerCharacter playerCharacter)
        {
           
            if (playerCharacter != null)
            {
                playerCharacter.TakeDamage(damage);
            }
        }
        
        // Méthode utilitaire pour afficher la zone de détection dans l'éditeur
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}