using UnityEngine;

namespace TP3_Polymorphisme
{
    public class Bow : Weapon
    {
        public override void Attack()
        {
            Debug.Log("Shooting an arrow for " + damage + " damage!");

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