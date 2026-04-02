using UnityEngine;

namespace TP3_Polymorphisme
{
    public class Knife : Weapon
    {
        protected override void OnAttack()
        {
            Debug.Log("Swinging sword");
            // Animation, effets sonores, etc.

            // Détection des ennemis à proximité
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (var hitCollider in hitColliders)
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    InflictDamages(Damages, enemy);
                }
            }
        }
    }
}
