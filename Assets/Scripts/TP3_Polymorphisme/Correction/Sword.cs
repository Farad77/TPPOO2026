using UnityEngine;
using System.Collections;

namespace TP3_Polymorphisme.Correction
{
    public class Sword : Weapon
    {
        [SerializeField] private float swingRadius = 2f;
        [SerializeField] private ParticleSystem slashEffect;
        [SerializeField] private float knockbackForce = 5f;

        // Point de référence pour l'attaque (optionnel)
        [SerializeField] private Transform attackPoint;

        // Paramètres pour l'animation de rotation
        [Header("Rotation Animation")]
        [SerializeField] private float rotationDuration = 0.5f;
        [SerializeField] private float rotationAngle = 360f;
        [SerializeField] private Vector3 rotationAxis = Vector3.up;
        [SerializeField] private bool useLocalRotation = true;

        private bool isAnimating = false;
        private Quaternion originalRotation;

        protected override void Awake()
        {
            base.Awake();
            WeaponName = "sword";
            damage = 25;
            attackRate = 1.5f;

            // Sauvegarder la rotation originale
            if (useLocalRotation)
                originalRotation = transform.localRotation;
            else
                originalRotation = transform.rotation;
        }

        // Obtenir le point de référence pour l'attaque
        private Vector3 GetAttackPoint()
        {
            if (attackPoint != null)
                return attackPoint.position;
            return transform.position;
        }

        public override void Attack()
        {
            if (!CanAttack() || isAnimating) return;

            Debug.Log("Swinging sword");
            PlayAttackSound();
            CreateAttackEffect();

            // Position centrale pour la détection
            Vector3 attackCenter = GetAttackPoint();

            // Lancer l'animation de rotation
            StartCoroutine(RotateAnimation());

            // Détection des ennemis dans la zone de frappe
            Collider[] hitColliders = Physics.OverlapSphere(attackCenter, swingRadius);

            // Ajouter des informations de débogage
            Debug.Log($"OverlapSphere détecte {hitColliders.Length} colliders dans un rayon de {swingRadius} à la position {attackCenter}");

            bool hitAnyEnemy = false;

            foreach (var hitCollider in hitColliders)
            {
                // Afficher tous les objets détectés pour debug
                Debug.Log($"Objet détecté: {hitCollider.gameObject.name}, Layer: {LayerMask.LayerToName(hitCollider.gameObject.layer)}");

                TP2_Heritage.Correction.Enemy enemy = hitCollider.GetComponent<TP2_Heritage.Correction.Enemy>();
                if (enemy != null)
                {
                    hitAnyEnemy = true;
                    Debug.Log($"Ennemi touché: {hitCollider.gameObject.name}");
                    enemy.TakeDamage(damage);

                    // Ajout d'un effet de recul
                    ApplyKnockback(enemy.transform);

                    // Dessiner une ligne de debug pendant l'exécution
                    Debug.DrawLine(attackCenter, enemy.transform.position, Color.red, 1.0f);
                }
            }

            if (!hitAnyEnemy)
            {
                Debug.Log("Aucun ennemi n'a été touché.");
            }
        }

        private IEnumerator RotateAnimation()
        {
            isAnimating = true;

            float startTime = Time.time;
            float endTime = startTime + rotationDuration;

            // Sauvegarde de la rotation de départ pour cette animation
            Quaternion startRotation;
            if (useLocalRotation)
                startRotation = transform.localRotation;
            else
                startRotation = transform.rotation;

            // Boucle d'animation
            while (Time.time < endTime)
            {
                // Calculer la progression (0 à 1)
                float progress = (Time.time - startTime) / rotationDuration;

                // Calculer l'angle de rotation actuel
                float currentAngle = progress * rotationAngle;

                // Méthode alternative: utiliser directement Euler angles pour une rotation plus prévisible
                if (useLocalRotation)
                {
                    // Définir la rotation autour de l'axe spécifié
                    if (rotationAxis == Vector3.up)
                        transform.localEulerAngles = startRotation.eulerAngles + new Vector3(0, currentAngle, 0);
                    else if (rotationAxis == Vector3.right)
                        transform.localEulerAngles = startRotation.eulerAngles + new Vector3(currentAngle, 0, 0);
                    else if (rotationAxis == Vector3.forward)
                        transform.localEulerAngles = startRotation.eulerAngles + new Vector3(0, 0, currentAngle);
                    else
                        transform.localRotation = startRotation * Quaternion.AngleAxis(currentAngle, rotationAxis);
                }
                else
                {
                    // Même chose mais en global
                    if (rotationAxis == Vector3.up)
                        transform.eulerAngles = startRotation.eulerAngles + new Vector3(0, currentAngle, 0);
                    else if (rotationAxis == Vector3.right)
                        transform.eulerAngles = startRotation.eulerAngles + new Vector3(currentAngle, 0, 0);
                    else if (rotationAxis == Vector3.forward)
                        transform.eulerAngles = startRotation.eulerAngles + new Vector3(0, 0, currentAngle);
                    else
                        transform.rotation = startRotation * Quaternion.AngleAxis(currentAngle, rotationAxis);
                }

                yield return null; // Attendre la prochaine frame
            }

            // S'assurer que l'animation se termine correctement
            if (useLocalRotation)
            {
                if (rotationAxis == Vector3.up)
                    transform.localEulerAngles = startRotation.eulerAngles + new Vector3(0, rotationAngle, 0);
                else if (rotationAxis == Vector3.right)
                    transform.localEulerAngles = startRotation.eulerAngles + new Vector3(rotationAngle, 0, 0);
                else if (rotationAxis == Vector3.forward)
                    transform.localEulerAngles = startRotation.eulerAngles + new Vector3(0, 0, rotationAngle);
                else
                    transform.localRotation = startRotation * Quaternion.AngleAxis(rotationAngle, rotationAxis);
            }
            else
            {
                if (rotationAxis == Vector3.up)
                    transform.eulerAngles = startRotation.eulerAngles + new Vector3(0, rotationAngle, 0);
                else if (rotationAxis == Vector3.right)
                    transform.eulerAngles = startRotation.eulerAngles + new Vector3(rotationAngle, 0, 0);
                else if (rotationAxis == Vector3.forward)
                    transform.eulerAngles = startRotation.eulerAngles + new Vector3(0, 0, rotationAngle);
                else
                    transform.rotation = startRotation * Quaternion.AngleAxis(rotationAngle, rotationAxis);
            }

            isAnimating = false;
        }

        protected override void CreateAttackEffect()
        {
            // Active l'effet de particules pour l'attaque
            if (slashEffect != null)
            {
                slashEffect.Play();
            }
        }

        private void ApplyKnockback(Transform target)
        {
            Rigidbody targetRb = target.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                Vector3 knockbackDirection = (target.position - GetAttackPoint()).normalized;
                targetRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }

        // Utiliser OnDrawGizmos au lieu de OnDrawGizmosSelected pour afficher les gizmos même en mode Play
        private void OnDrawGizmos()
        {
            // Utiliser la même position pour les gizmos que pour la détection
            Vector3 gizmoCenter = GetAttackPoint();

            // Dessiner la sphère de détection en rouge
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gizmoCenter, swingRadius);

            // Dessiner une sphère pleine semi-transparente pour mieux visualiser
            Gizmos.color = new Color(1, 0, 0, 0.2f); // Rouge semi-transparent
            Gizmos.DrawSphere(gizmoCenter, swingRadius);

            // Dessiner les axes locaux pour mieux comprendre l'orientation
            float axisLength = swingRadius * 0.5f;

            Gizmos.color = Color.red; // Axe X
            Gizmos.DrawRay(gizmoCenter, transform.right * axisLength);

            Gizmos.color = Color.green; // Axe Y
            Gizmos.DrawRay(gizmoCenter, transform.up * axisLength);

            Gizmos.color = Color.blue; // Axe Z
            Gizmos.DrawRay(gizmoCenter, transform.forward * axisLength);
        }
    }
}