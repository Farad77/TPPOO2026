using System.Collections;
using UnityEngine;

namespace TP2_Heritage.Correction
{
    public class Boss : Enemy
    {
        [SerializeField] private float enrageHealthThreshold = 0.3f; // 30% de sa vie
        [SerializeField] private float damageMultiplierWhenEnraged = 1.5f;
        [SerializeField] private float speedMultiplierWhenEnraged = 1.3f;
        [SerializeField] private GameObject minion; // Ennemi à invoquer
        [SerializeField] private int maxMinions = 3;
        [SerializeField] private float summonCooldown = 10f;
        
        private int maxHealth;
        private bool isEnraged = false;
        private float lastSummonTime;
        private int currentMinionCount = 0;
        
        protected override void InitializeStats()
        {
            health = 300;
            maxHealth = health;
            damage = 25;
            speed = 1f;
            detectionRange = 50f;
        }
        
        protected override void Update()
        {
            base.Update();
            
            // Vérifie s'il doit entrer en rage
            if (!isEnraged && health <= maxHealth * enrageHealthThreshold)
            {
                EnterEnragedState();
            }
            
            // Invoque des sbires régulièrement
            if (Time.time - lastSummonTime > summonCooldown && currentMinionCount < maxMinions)
            {
                SummonMinion();
                lastSummonTime = Time.time;
            }
        }
        
        private void EnterEnragedState()
        {
            isEnraged = true;
            damage = Mathf.RoundToInt(damage * damageMultiplierWhenEnraged);
            speed *= speedMultiplierWhenEnraged;
            
            Debug.Log("Le boss entre en état de rage !");
            
            // Effets visuels (à implémenter)
            // GetComponent<Renderer>().material.color = Color.red;
        }
        
        private void SummonMinion()
        {
            if (minion != null)
            {
                // Position aléatoire autour du boss
                Vector2 randomCircle = Random.insideUnitCircle * 3f;
                Vector3 spawnPosition = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
                
                GameObject spawnedMinion = Instantiate(minion, spawnPosition, Quaternion.identity);
                
                // Garde une référence pour savoir combien de sbires sont actifs
                MinionTracker tracker = spawnedMinion.AddComponent<MinionTracker>();
                tracker.boss = this;
                
                currentMinionCount++;
                
                Debug.Log("Le boss invoque un sbire !");
            }
        }
        
        public void MinionDied()
        {
            currentMinionCount = Mathf.Max(0, currentMinionCount - 1);
        }
        
        protected override void OnDamageReceived(int amount)
        {
            base.OnDamageReceived(amount);
            
            // Le boss a des phases en fonction de sa santé
            float healthPercentage = (float)health / maxHealth;
            if (healthPercentage < 0.5f && healthPercentage > 0.3f)
            {
                Debug.Log("Le boss devient plus agressif !");
                // Changement de comportement à mi-vie
            }
        }
        
        // Attaque de zone
        protected  void DealDamageToPlayer()
        {
            // Le boss inflige des dégâts en zone
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    TP1_Encapsulation.Correction.PlayerCharacter playerCharacter = hitCollider.GetComponent<TP1_Encapsulation.Correction.PlayerCharacter>();
                    if (playerCharacter != null)
                    {
                        playerCharacter.TakeDamage(damage);
                    }
                }
            }
        }
        
        // Mort spéciale du boss avec récompense
        protected override void Die()
        {
            Debug.Log("Le boss est vaincu ! Le donjon est libéré !");
            
            // Donne une récompense au joueur
            TP1_Encapsulation.Correction.PlayerCharacter player = GameObject.FindGameObjectWithTag("Player").GetComponent<TP1_Encapsulation.Correction.PlayerCharacter>();
            if (player != null)
            {
                player.GainGold(100);
                // Autres récompenses possibles
            }
            
            // Effet visuel de mort
            // StartCoroutine(DeathEffect());
            
            base.Die();
        }
        
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            
            // Affiche également la zone d'attaque
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 3f);
        }

        // Implémentation de la méthode abstraite
        protected override void DealDamageToPlayer(TP1_Encapsulation.Correction.PlayerCharacter player)
        {
            player.TakeDamage(damage);
            Debug.Log($"Le boss écrase le joueur pour {damage} dégâts!");
        }
    }
    
    // Classe utilitaire pour suivre les sbires
    public class MinionTracker : MonoBehaviour
    {
        public Boss boss;
        
        private void OnDestroy()
        {
            if (boss != null)
            {
                boss.MinionDied();
            }
        }
    }
}