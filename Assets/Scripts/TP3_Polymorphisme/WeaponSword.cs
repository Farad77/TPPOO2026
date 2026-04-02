using UnityEngine;

public class WeaponSword : Weapon
{
    private void Start()
    {
        _damage = 25;
    }


    private float NextUse;

    public override void Attack()
    {
        if (currentWeapon == "sword")
        {
            // Logique d'attaque à l'épée
            Debug.Log("Swinging sword");
            // Animation, effets sonores, etc.

            // Détection des ennemis à proximité
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (var hitCollider in hitColliders)
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(_damage);
                }
            }
        }
    }
}
