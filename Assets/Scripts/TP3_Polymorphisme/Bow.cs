using UnityEngine;

namespace TP3_Polymorphisme
{
    public class Bow : Weapon
    {
        protected override void OnAttack()
        {
            /*Enemy target = null;
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo);
            if (hitInfo.collider.GetComponent<Enemy>())
            {
                target = hitInfo.collider.GetComponent<Enemy>();

                InflictDamages(Damages, target);
            }*/

            // Logique d'attaque à l'arc
            Debug.Log("Firing arrow");

            // Création d'une flèche
            GameObject arrowPrefab = Resources.Load<GameObject>("Arrow");
            if (arrowPrefab != null)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position + transform.forward, Quaternion.identity);
                Rigidbody rb = arrow.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = transform.forward * 20f;
                }
            }
        }
    }
}
