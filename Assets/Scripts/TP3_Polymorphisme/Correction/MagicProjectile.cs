
    using UnityEngine;
    using System.Collections;
    namespace TP3_Polymorphisme.Correction
    {
        public class MagicProjectile : MonoBehaviour
        {
            [SerializeField] private float lifeTime = 5f;
            [SerializeField] private ParticleSystem impactEffect;
            [SerializeField] private AudioClip impactSound;
            [SerializeField] private float explosionRadius = 0f; // 0 = pas d'explosion

            private int damage = 0;
            private bool hasHit = false;
            private AudioSource audioSource;
            private Renderer projectileRenderer;

            private void Awake()
            {
                // Obtenir les composants nécessaires
                audioSource = GetComponent<AudioSource>();
                projectileRenderer = GetComponentInChildren<Renderer>();

                // Détruire le projectile après un certain temps s'il ne touche rien
                Destroy(gameObject, lifeTime);
            }

            public void SetDamage(int newDamage)
            {
                damage = newDamage;
            }

            public void SetColor(Color newColor)
            {
                // Mettre à jour la couleur du renderer si disponible
                if (projectileRenderer != null && projectileRenderer.material != null)
                {
                    projectileRenderer.material.color = newColor;

                    // Si le projectile a un système de particules, mettre à jour sa couleur aussi
                    ParticleSystem particleSystem = GetComponent<ParticleSystem>();
                    if (particleSystem != null)
                    {
                        var main = particleSystem.main;
                        main.startColor = newColor;
                    }

                    // Ajouter une lumière si nécessaire
                    Light light = GetComponent<Light>();
                    if (light != null)
                    {
                        light.color = newColor;
                    }
                }
            }

            private void OnCollisionEnter(Collision collision)
            {
                if (hasHit) return; // Éviter des collisions multiples
                hasHit = true;

                // Gérer l'impact
                HandleImpact(collision.contacts[0].point, collision.gameObject);
            }

            private void OnTriggerEnter(Collider other)
            {
                if (hasHit) return;
                hasHit = true;

                // Gérer l'impact
                HandleImpact(transform.position, other.gameObject);
            }

            private void HandleImpact(Vector3 impactPoint, GameObject hitObject)
            {
                // Appliquer les dégâts à la cible
                IDamageable damageable = hitObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                }

                // Vérifier s'il y a un effet d'explosion
                if (explosionRadius > 0)
                {
                    // Trouver tous les objets dans le rayon d'explosion
                    Collider[] colliders = Physics.OverlapSphere(impactPoint, explosionRadius);
                    foreach (Collider nearbyObject in colliders)
                    {
                        // Appliquer des dégâts aux objets endommageables dans le rayon
                        IDamageable nearbyDamageable = nearbyObject.GetComponent<IDamageable>();
                        if (nearbyDamageable != null && nearbyObject.gameObject != hitObject)
                        {
                            // Calculer des dégâts réduits basés sur la distance
                            float distance = Vector3.Distance(impactPoint, nearbyObject.transform.position);
                            float damagePercent = 1 - (distance / explosionRadius);
                            int explosionDamage = Mathf.RoundToInt(damage * damagePercent);

                            // Appliquer les dégâts
                            nearbyDamageable.TakeDamage(explosionDamage);
                        }
                    }
                }

                // Créer l'effet d'impact
                if (impactEffect != null)
                {
                    ParticleSystem effect = Instantiate(impactEffect, impactPoint, Quaternion.identity);
                    // Configurer l'effet avec la couleur du projectile si possible
                    if (projectileRenderer != null)
                    {
                        var main = effect.main;
                        main.startColor = projectileRenderer.material.color;
                    }
                }

                // Jouer le son d'impact
                if (impactSound != null && audioSource != null)
                {
                    // Détacher l'audio source pour qu'il puisse finir de jouer le son
                    audioSource.transform.SetParent(null);
                    audioSource.PlayOneShot(impactSound);
                    Destroy(audioSource.gameObject, impactSound.length);
                }

                // Détruire le projectile
                Destroy(gameObject);
            }

            // Méthode pour visualiser le rayon d'explosion dans l'éditeur
            private void OnDrawGizmosSelected()
            {
                if (explosionRadius > 0)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(transform.position, explosionRadius);
                }
            }
        }
    }
