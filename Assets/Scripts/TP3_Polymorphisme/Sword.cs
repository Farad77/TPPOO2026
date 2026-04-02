using UnityEngine;

namespace TP2_Heritage.Correction
{
    public class Sword : Weapon
    {
        public override void Attack()
        {
            Debug.Log("Swinging sword");
            // Animation, effets sonores, etc.

            // Détection des ennemis à proximité
            Collider[] hitColliders = new Collider[100];
            if (Physics.OverlapSphereNonAlloc(transform.position, 2f, hitColliders) > 0)
            {
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider != null)
                    {
                        if (hitCollider.TryGetComponent<Enemy>(out Enemy enemy)) enemy.TakeDamage(25);
                    }
                }
            }
        }

        public override void Cooldown()
        {
        }
    }
}

