using UnityEngine;

namespace TP3_Polymorphisme
{
    public class WeaponManager : MonoBehaviour
    {
        public GameObject sword;
        public GameObject bow;
        public GameObject wand;
        
        private string currentWeapon = "sword";
        
        public void Attack()
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
                        enemy.TakeDamage(25);
                    }
                }
            }
            else if (currentWeapon == "bow")
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
            else if (currentWeapon == "wand")
            {
                // Logique d'attaque à la baguette
                Debug.Log("Casting spell");
                
                // Vérifier si le joueur a assez de mana
                PlayerCharacter player = GetComponent<PlayerCharacter>();
                if (player != null && player.SpendMana(15f))
                {
                    // Création d'un projectile magique
                    GameObject spellPrefab = Resources.Load<GameObject>("Spell");
                    if (spellPrefab != null)
                    {
                        GameObject spell = Instantiate(spellPrefab, transform.position + transform.forward, Quaternion.identity);
                        spell.GetComponent<Rigidbody>().linearVelocity = transform.forward * 15f;
                    }
                }
                else
                {
                    Debug.Log("Not enough mana!");
                }
            }
        }
        
        public void SwitchWeapon(string weaponName)
        {
            currentWeapon = weaponName;
            
            sword.SetActive(currentWeapon == "sword");
            bow.SetActive(currentWeapon == "bow");
            wand.SetActive(currentWeapon == "wand");
            
            Debug.Log("Switched to " + currentWeapon);
        }
    }
}