using UnityEngine;

namespace TP3_Polymorphisme
{
    public class Wand : Weapon
    {
        public override void Attack()
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
                else
                {
                    Debug.Log("Not enough mana!");
                }
            }
        }

        public override void Cooldown()
        {
        }
    }
}

