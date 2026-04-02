using UnityEngine;

namespace TP2_Heritage.Correction
{
    public class Zombie : Enemy
    {
        [SerializeField] private float infectionRange = 2f;
        [SerializeField] private int infectionDamage = 5;
        
        protected override void InitializeStats()
        {
            health = 100;
            damage = 10;
            speed = 2f;
            detectionRange = 10f;
        }
        
        // Comportement spécifique au zombie
        protected override void Update()
        {
            base.Update();
            
            // Ajouter une vérification d'infection pour les joueurs proches
            if (player != null && Vector3.Distance(transform.position, player.position) < infectionRange)
            {
                InfectNearbyPlayer();
            }
        }
        
        private void InfectNearbyPlayer()
        {
            // Simule un effet de dégât sur la durée (poison/infection)
            TP1_Encapsulation.Correction.PlayerCharacter playerCharacter = player.GetComponent<TP1_Encapsulation.Correction.PlayerCharacter>();
            if (playerCharacter != null)
            {
                // On pourrait ajouter un effet de statut ici
                if (Random.value < 0.01f) // Chance de 1% par frame
                {
                    playerCharacter.TakeDamage(infectionDamage);
                    Debug.Log("Le joueur a été infecté !");
                }
            }
        }
        
        // Animation spécifique pour les zombies
        protected override void Die()
        {
            Debug.Log("Le zombie s'effondre !");
            // On pourrait ajouter une animation spécifique, des particules, etc.
            
            base.Die();
        }
        
        // Les zombies font un son spécifique quand ils reçoivent des dégâts
        protected override void OnDamageReceived(int amount)
        {
            base.OnDamageReceived(amount);
            Debug.Log("Le zombie grogne de douleur !");
            // On pourrait ajouter un son spécifique ici
        }
        
        // Implémentation de la méthode abstraite
        protected override void DealDamageToPlayer(TP1_Encapsulation.Correction.PlayerCharacter player)
        {
            player.TakeDamage(damage);
            Debug.Log($"Le zombie mord le joueur pour {damage} dégâts!");
        }
    }
}