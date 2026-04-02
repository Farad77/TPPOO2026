using UnityEngine;
namespace TP3_Polymorphisme.Correction
{
    public class Axe : Weapon
    {
        [SerializeField] private float swingRadius = 2.5f;
        [SerializeField] private float throwSpeed = 15f;
        [SerializeField] private float returnDelay = 1.5f;
        [SerializeField] private ParticleSystem swingEffect;

        private bool isThrowing = false;
        private GameObject thrownAxe = null;
        private Vector3 originalPosition;
        private Quaternion originalRotation;

        protected override void Awake()
        {
            base.Awake();
            WeaponName = "axe";
            damage = 35;
            attackRate = 0.7f; // Attaques par seconde

            // Enregistre la position et rotation initiales
            originalPosition = transform.localPosition;
            originalRotation = transform.localRotation;
        }

        public override void Attack()
        {
            if (!CanAttack()) return;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Attaque spéciale : lancer de hache
                ThrowAxe();
            }
            else
            {
                // Attaque normale : coup de hache
                NormalAttack();
            }
        }

        private void NormalAttack()
        {
            if (isThrowing) return;

            Debug.Log("Swinging axe");
            PlayAttackSound();
            CreateAttackEffect();

            // Détection des ennemis dans la zone de frappe
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, swingRadius);
            foreach (var hitCollider in hitColliders)
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);

                    // La hache peut étourdir les ennemis
                    if (Random.value < 0.3f) // 30% de chance
                    {
                        StunEnemy(enemy);
                    }
                }
            }
        }

        private void ThrowAxe()
        {
            if (isThrowing) return;

            Debug.Log("Throwing axe");
            PlayAttackSound();

            // Cache la hache sur le joueur
            gameObject.SetActive(false);

            // Crée une instance de la hache à lancer
            thrownAxe = Instantiate(gameObject, transform.position, transform.rotation);
            thrownAxe.SetActive(true);

            // Ajuste les composants pour qu'elle fonctionne comme un projectile
            Collider thrownAxeCollider = thrownAxe.GetComponent<Collider>();
            if (thrownAxeCollider != null)
            {
                thrownAxeCollider.isTrigger = true;
            }

            Rigidbody rb = thrownAxe.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.linearVelocity = transform.parent.forward * throwSpeed;

            // Ajoute un effet de rotation
            rb.angularVelocity = new Vector3(0, 0, 10f);

            // Ajoute un gestionnaire de collision
            ThrownAxe axeComponent = thrownAxe.AddComponent<ThrownAxe>();
            axeComponent.Initialize(this, damage);

            isThrowing = true;

            // Planifie le retour de la hache
            Invoke("ReturnAxe", returnDelay);
        }

        private void ReturnAxe()
        {
            if (thrownAxe != null)
            {
                Destroy(thrownAxe);
            }

            // Restaure la hache sur le joueur
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
            gameObject.SetActive(true);
            isThrowing = false;
        }

        private void StunEnemy(Enemy enemy)
        {
            // Pourrait être implémenté si l'ennemi a une méthode pour être étourdi
            Debug.Log($"Enemy {enemy.name} is stunned!");
        }

        protected override void CreateAttackEffect()
        {
            // Active l'effet de particules pour l'attaque
            if (swingEffect != null)
            {
                swingEffect.Play();
            }
        }

        protected override void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, swingRadius);
        }

        protected override void OnPickupEffect(GameObject collector)
        {
            throw new System.NotImplementedException();
        }
    }

    // Classe pour gérer la hache lancée
    public class ThrownAxe : MonoBehaviour
    {
        private Axe parentAxe;
        private int damage;

        public void Initialize(Axe axe, int weaponDamage)
        {
            parentAxe = axe;
            damage = weaponDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Dégâts augmentés lors du lancer
                    enemy.TakeDamage(damage * 2);
                    Debug.Log($"Thrown axe hit {enemy.name}!");
                }
            }
        }
    }
}