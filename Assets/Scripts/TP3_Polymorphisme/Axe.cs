using UnityEngine;

namespace TP3_Polymorphisme
{
    public class Axe : Weapon
    {
        public override void Attack()
        {
            Debug.Log("Swinging the axe for " + damage + "damage!");

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (var hitCollider in hitColliders)
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}