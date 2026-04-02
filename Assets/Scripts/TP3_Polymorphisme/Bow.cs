using TP3_Polymorphisme_Nicolas;
using UnityEngine;

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
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = transform.forward * 20f;
            }
        }
    }

}
