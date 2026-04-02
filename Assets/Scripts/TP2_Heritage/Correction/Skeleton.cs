using UnityEngine;

namespace TP2_Heritage.Correction
{
    public class Skeleton : Enemy
    {
        [SerializeField] private float shootRange = 8f;
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private float shootCooldown = 2f;
        private float lastShootTime;
        
        protected override void InitializeStats()
        {
            health = 80;
            damage = 15;
            speed = 3f;
            detectionRange = 12f;
        }
        
        protected override void Update()
        {
            // Si le joueur est à portée de tir, s'arrête et tire
            if (player != null && Vector3.Distance(transform.position, player.position) < shootRange)
            {
                // S'arrête et tire
                if (Time.time - lastShootTime > shootCooldown)
                {
                    ShootArrow();
                    lastShootTime = Time.time;
                }
            }
            else
            {
                // Sinon utilise le comportement standard
                base.Update();
            }
        }
        
        private void ShootArrow()
        {
            if (arrowPrefab != null)
            {
                Debug.Log("Le squelette tire une flèche !");
                
                Vector3 direction = (player.position - transform.position).normalized;
                GameObject arrow = Instantiate(arrowPrefab, transform.position + direction, Quaternion.identity);
                
                // Configure la flèche
                Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
                if (arrowRb != null)
                {
                    arrowRb.linearVelocity = direction * 10f;
                }
                
                // Configure les dégâts
                Arrow arrowComponent = arrow.GetComponent<Arrow>();
                if (arrowComponent != null)
                {
                    arrowComponent.Damage = damage ;
                }
            }
        }
        
        // Les squelettes ont une animation différente à la mort
        protected override void Die()
        {
            Debug.Log("Le squelette s'effondre en un tas d'os !");
            // On pourrait ajouter une animation démembrement, des particules, etc.
            
            base.Die();
        }
        
        // Le squelette garde ses distances
        protected override void MoveTowardsPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            
            // Garde une distance de sécurité
            if (distanceToPlayer < shootRange * 0.7f)
            {
                // Recule
                Vector3 direction = (transform.position - player.position).normalized;
                transform.position += direction * speed * 0.5f * Time.deltaTime;
            }
            else if (distanceToPlayer > shootRange)
            {
                // Avance
                base.MoveTowardsPlayer();
            }
        }
        
        // Implémentation de la méthode abstraite
        protected override void DealDamageToPlayer(TP1_Encapsulation.Correction.PlayerCharacter player)
        {
            player.TakeDamage(damage);
            Debug.Log($"Le squelette attaque avec ses os tranchants pour {damage} dégâts!");
        }
    }
}