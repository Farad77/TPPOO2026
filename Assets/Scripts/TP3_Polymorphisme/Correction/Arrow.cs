using UnityEngine;

namespace TP3_Polymorphisme.Correction
{ public class Arrow : MonoBehaviour
    {
        [SerializeField] private int damage = 10;
        [SerializeField] private float lifetime = 5f;
        [SerializeField] private AudioClip impactSound;
        [SerializeField] private ParticleSystem impactEffect;
        
        private Rigidbody arrowRigidbody;
        private bool hasHit = false;
        
        // Propriétés avec accesseurs pour encapsuler les variables
        public int Damage
        {
            get { return damage; }
             set { damage = value; }
        }
        
        public float Lifetime
        {
            get { return lifetime; }
            private set { lifetime = Mathf.Max(0, value); } // Empêche les valeurs négatives
        }
        
        private void Awake()
        {
            // Récupérer les références des composants
            arrowRigidbody = GetComponent<Rigidbody>();
            
            // Vérification des références
            if (arrowRigidbody == null)
            {
                Debug.LogError("Arrow requires a Rigidbody component");
            }
        }
        
        private void Start()
        {
            // Autodestruction après un certain temps
            Destroy(gameObject, lifetime);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // Éviter plusieurs collisions
            if (hasHit) return;
            
            // Inflige des dégâts au joueur si touché
            if (other.CompareTag("Player"))
            {
                ApplyDamageToPlayer(other);
            }
            else if (!other.CompareTag("Enemy")) // Ne pas interagir si collision avec un ennemi
            {
                StickToSurface();
            }
        }
        
        // Méthode encapsulée pour appliquer les dégâts au joueur
        private void ApplyDamageToPlayer(Collider playerCollider)
        {
            TP1_Encapsulation.Correction.PlayerCharacter player = playerCollider.GetComponent<TP1_Encapsulation.Correction.PlayerCharacter>();
            if (player != null)
            {
                player.TakeDamage(damage);
                PlayImpactEffect(playerCollider.transform.position);
                hasHit = true;
                Destroy(gameObject); // La flèche est détruite après avoir touché
            }
        }
        
        // Méthode encapsulée pour gérer le comportement de la flèche qui se plante
        private void StickToSurface()
        {
            if (arrowRigidbody != null)
            {
                arrowRigidbody.isKinematic = true;
                arrowRigidbody.linearVelocity = Vector3.zero;
                arrowRigidbody.angularVelocity = Vector3.zero;
            }
            
            // Marquer comme touchée pour éviter d'autres interactions
            hasHit = true;
            
            // Jouer l'effet d'impact
            PlayImpactEffect(transform.position);
        }
        
        // Méthode pour jouer les effets visuels et sonores d'impact
        private void PlayImpactEffect(Vector3 position)
        {
            // Jouer un effet de particules si disponible
            if (impactEffect != null)
            {
                Instantiate(impactEffect, position, Quaternion.identity);
            }
            
            // Jouer un son d'impact si disponible
            if (impactSound != null)
            {
                AudioSource.PlayClipAtPoint(impactSound, position);
            }
        }
        
        // Méthode publique pour définir les dégâts (utile pour les flèches spéciales)
        public void SetDamage(int newDamage)
        {
            Damage = newDamage;
        }
    }
}
