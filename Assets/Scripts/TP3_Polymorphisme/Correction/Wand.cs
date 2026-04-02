using TP3_Polymorphisme.Correction;
using UnityEngine;

namespace TP3_Polymorphisme.Correction
{
    public class Wand : Weapon
    {
        [SerializeField] private GameObject spellPrefab;
        [SerializeField] private Transform spellSpawnPoint;
        [SerializeField] private float spellSpeed = 15f;
        [SerializeField] private float manaCost = 15f;
        [SerializeField] private ParticleSystem castEffect;
        [SerializeField] private Color spellColor = Color.blue;

        protected override void Awake()
        {
            base.Awake();
            WeaponName = "wand";
            damage = 30;
            attackRate = 0.5f; // Attaques par seconde
        }

        public override void Attack()
        {
            if (!CanAttack()) return;

            // Vérifier si le joueur a assez de mana
            PlayerCharacter player = GetComponentInParent<PlayerCharacter>();
            if (player != null && player.SpendMana(manaCost))
            {
                Debug.Log("Casting spell");
                PlayAttackSound();
                CreateAttackEffect();

                if (spellPrefab != null && spellSpawnPoint != null)
                {
                    GameObject spell = Instantiate(spellPrefab, spellSpawnPoint.position, spellSpawnPoint.rotation);

                    // Configuration de base du projectile
                    Rigidbody rb = spell.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = spellSpawnPoint.forward * spellSpeed;
                    }

                    // Configure le projectile avec les dégâts
                    MagicProjectile projectile = spell.GetComponent<MagicProjectile>();
                    if (projectile != null)
                    {
                        projectile.SetDamage(damage);
                        projectile.SetColor(spellColor);
                    }
                }
            }
            else
            {
                Debug.Log("Not enough mana!");
            }
        }

        protected override void CreateAttackEffect()
        {
            // Active l'effet de particules pour le lancement du sort
            if (castEffect != null)
            {
                castEffect.Play();

                // Change la couleur des particules pour correspondre au sort
                var main = castEffect.main;
                main.startColor = spellColor;
            }
        }

        // Méthode spécifique à la baguette pour changer le type de sort
        public void ChangeSpellType(SpellType type)
        {
            switch (type)
            {
                case SpellType.Fire:
                    spellColor = Color.red;
                    damage = 35;
                    manaCost = 20f;
                    break;
                case SpellType.Ice:
                    spellColor = Color.cyan;
                    damage = 25;
                    manaCost = 10f;
                    break;
                case SpellType.Lightning:
                    spellColor = Color.yellow;
                    damage = 40;
                    manaCost = 25f;
                    break;
                default:
                    spellColor = Color.blue;
                    damage = 30;
                    manaCost = 15f;
                    break;
            }

            Debug.Log($"Changed spell type to {type}");
        }

        protected override void OnPickupEffect(GameObject collector)
        {
            throw new System.NotImplementedException();
        }

        // Types de sorts disponibles
        public enum SpellType
        {
            Standard,
            Fire,
            Ice,
            Lightning
        }
    }
}