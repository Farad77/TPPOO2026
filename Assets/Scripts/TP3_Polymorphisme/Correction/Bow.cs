using UnityEngine;
using System.Collections.Generic;
using TP2_Heritage.Correction; // Utiliser le namespace de votre classe Arrow existante

namespace TP3_Polymorphisme.Correction
{
    public class Bow : Weapon
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform arrowSpawnPoint;
        [SerializeField] private float arrowSpeed = 20f;
        [SerializeField] private ParticleSystem stringEffect;
        [SerializeField] private int maxArrows = 20;

        [Header("Enemy Targeting")]
        [SerializeField] private float detectionRadius = 10f; // Rayon de détection des ennemis
        [SerializeField] private LayerMask enemyLayerMask; // Layer des ennemis
        [SerializeField] private bool showTargetingDebug = true; // Afficher le debug du ciblage
        [SerializeField] private float rotationSpeed = 10f; // Vitesse de rotation de l'arc
        [SerializeField] private bool smoothRotation = true; // Rotation progressive ou instantanée

        private int currentArrows;
        private Transform currentTarget = null;
        private Quaternion defaultRotation;
        private bool isRotating = false;

        protected override void Awake()
        {
            base.Awake();
            WeaponName = "bow";
            damage = 15;
            attackRate = 2f; // Attaques par seconde
            currentArrows = maxArrows;

            // Sauvegarder la rotation par défaut
            defaultRotation = transform.rotation;
        }

        private void Update()
        {
            // Chercher l'ennemi le plus proche en permanence
            Transform target = FindClosestEnemy();

            // Si on a trouvé une cible, tourner l'arc vers elle
            if (target != null)
            {
                currentTarget = target;
                RotateBowTowardsTarget();
            }
            else if (currentTarget == null && isRotating)
            {
                // Revenir à la rotation par défaut s'il n'y a plus de cible
                if (smoothRotation)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, Time.deltaTime * rotationSpeed);

                    // Vérifier si on est presque revenu à la rotation par défaut
                    if (Quaternion.Angle(transform.rotation, defaultRotation) < 0.1f)
                    {
                        transform.rotation = defaultRotation;
                        isRotating = false;
                    }
                }
                else
                {
                    transform.rotation = defaultRotation;
                    isRotating = false;
                }
            }
        }

        // Faire tourner l'arc vers la cible
        private void RotateBowTowardsTarget()
        {
            if (currentTarget == null) return;

            // Calculer la direction vers la cible
            Vector3 targetDirection = (currentTarget.position - transform.position).normalized;

            // Ignorer la composante Y pour une rotation seulement horizontale
            // Décommentez cette ligne si vous voulez une rotation uniquement horizontale
            // targetDirection.y = 0;

            // Calculer la rotation pour regarder vers la cible
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            isRotating = true;

            // Appliquer la rotation (progressive ou instantanée)
            if (smoothRotation)
            {
                // Rotation progressive
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                // Rotation instantanée
                transform.rotation = targetRotation;
            }
        }

        public override void Attack()
        {
            if (!CanAttack() || currentArrows <= 0) return;

            // Utiliser la cible actuelle (déjà suivie par l'arc)
            Transform targetEnemy = currentTarget;

            Debug.Log(targetEnemy != null
                ? $"Firing arrow at {targetEnemy.name}"
                : "Firing arrow (no target)");

            PlayAttackSound();
            CreateAttackEffect();

            // Création et lancement de la flèche
            if (arrowPrefab != null && arrowSpawnPoint != null)
            {
                // Direction de tir (vers l'ennemi ou tout droit dans la direction -Z)
                Vector3 shootDirection;
                Quaternion shootRotation;

                if (targetEnemy != null)
                {
                    // Calculer la direction vers l'ennemi
                    Vector3 targetDirection = (targetEnemy.position - arrowSpawnPoint.position).normalized;
                    shootDirection = targetDirection;

                    // Créer une rotation qui oriente la flèche vers l'ennemi
                    // mais avec le forward de la flèche aligné sur le -Z
                    shootRotation = Quaternion.LookRotation(targetDirection);

                    // Rotation supplémentaire pour que le -Z de la flèche pointe vers l'ennemi
                    shootRotation *= Quaternion.Euler(0, 180, 0);

                    if (showTargetingDebug)
                    {
                        // Dessiner une ligne de debug vers l'ennemi
                        Debug.DrawLine(arrowSpawnPoint.position, targetEnemy.position, Color.red, 1.0f);
                    }
                }
                else
                {
                    // Tirer tout droit dans la direction -Z du point de spawn
                    shootDirection = -arrowSpawnPoint.forward; // Utiliser -Z comme direction

                    // Utiliser la rotation du point de spawn mais inverser Z
                    shootRotation = arrowSpawnPoint.rotation * Quaternion.Euler(0, 180, 0);
                }

                // Créer la flèche avec la rotation calculée
                GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, shootRotation);

                // Configurer la flèche
                Rigidbody rb = arrow.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = shootDirection * arrowSpeed;
                }

                // Configure la flèche avec les dégâts
                Arrow arrowComponent = arrow.GetComponent<Arrow>();
                if (arrowComponent != null)
                {
                    arrowComponent.Damage = damage;
                }

                // Réduit le nombre de flèches disponibles
                currentArrows--;
                if (currentArrows <= 0)
                {
                    Debug.Log("Out of arrows!");
                }
            }
        }

        // Trouve l'ennemi le plus proche dans le rayon de détection
        private Transform FindClosestEnemy()
        {
            // Rechercher tous les colliders dans le rayon
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);

            if (hitColliders.Length == 0)
                return null;

            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (var hitCollider in hitColliders)
            {
                // Vérifier si c'est un ennemi
                TP2_Heritage.Correction.Enemy enemy = hitCollider.GetComponent<TP2_Heritage.Correction.Enemy>();
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);

                    // Vérifier si c'est le plus proche jusqu'à présent
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = hitCollider.transform;
                    }
                }
            }

            // Si on a trouvé un ennemi et que le debug est activé
            if (showTargetingDebug && closestEnemy != null)
            {
                // Dessiner une sphère autour de l'ennemi ciblé
                Debug.DrawLine(transform.position, closestEnemy.position, Color.yellow, 0.2f);
            }

            return closestEnemy;
        }

        protected override void CreateAttackEffect()
        {
            // Active l'effet de la corde d'arc
            if (stringEffect != null)
            {
                stringEffect.Play();
            }
        }

        // Méthode spécifique à l'arc pour recharger les flèches
        public void Reload(int amount)
        {
            currentArrows = Mathf.Min(currentArrows + amount, maxArrows);
            Debug.Log($"Reloaded {amount} arrows. Current arrows: {currentArrows}");
        }

        // Accesseur pour obtenir le nombre de flèches
        public int GetArrowCount()
        {
            return currentArrows;
        }

        protected override void OnPickupEffect(GameObject collector)
        {
            // Chercher le WeaponManager sur le joueur
            WeaponManager weaponManager = collector.GetComponent<WeaponManager>();

            if (weaponManager != null)
            {
                // D'abord désactiver l'arme car le WeaponManager gère l'activation
                gameObject.SetActive(false);

                // Détacher l'arme de sa position actuelle et l'attacher au joueur
                transform.SetParent(weaponManager.transform);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                // Ajouter l'arme au manager
                weaponManager.AddWeapon(this, true);

                Debug.Log($"Le joueur a ramassé l'arme: {WeaponName}");
            }
            else
            {
                Debug.LogWarning("Le joueur n'a pas de WeaponManager.");
            }
        }

        // Affichage des gizmos pour visualiser le rayon de détection
        private void OnDrawGizmos()
        {
            // Dessiner la sphère de détection
            Gizmos.color = new Color(1f, 1f, 0f, 0.2f); // Jaune semi-transparent
            Gizmos.DrawSphere(transform.position, detectionRadius);

            // Dessiner le contour de la sphère
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            // Visualiser le point de spawn de la flèche
            if (arrowSpawnPoint != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(arrowSpawnPoint.position, 0.1f);
                Gizmos.DrawRay(arrowSpawnPoint.position, arrowSpawnPoint.forward * 1f);
            }
        }
    }
}