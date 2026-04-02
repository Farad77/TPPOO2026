using UnityEngine;

namespace TP3_Polymorphisme
{
    public class Bow : Weapon
    {
        public override void Attack()
        {
            // Logique d'attaque à l'arc
            Debug.Log("Firing arrow");

            // Création d'une flèche
            GameObject arrowPrefab = Resources.Load<GameObject>("Arrow");
            if (arrowPrefab != null)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position + transform.forward, Quaternion.identity);
                if (arrow.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.linearVelocity = transform.forward * 20f;
            }
        }

        public override void Cooldown()
        {
        }
    }
}

