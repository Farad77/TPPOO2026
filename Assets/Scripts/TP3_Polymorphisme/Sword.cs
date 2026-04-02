using TP3_Polymorphisme_Nicolas;
using UnityEngine;

public class Sword : Weapon
{
    public override void Attack()
    {
        Debug.Log("Swinging sword");
        // Animation, effets sonores, etc.

        // Détection des ennemis à proximité
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            TP2_Heritage.Correction.Enemy enemy = hitCollider.GetComponent<TP2_Heritage.Correction.Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(25);
            }
        }
    }

}
