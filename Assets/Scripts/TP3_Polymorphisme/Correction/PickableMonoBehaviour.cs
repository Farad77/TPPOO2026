using UnityEngine;

// Classe de base pour tous les objets ramassables
// Cette classe est conçue pour être héritée par d'autres classes comme Weapon
public abstract class PickableMonoBehaviour : MonoBehaviour, IPickable
{
    [Header("Pickable Settings")]
    [SerializeField] protected string pickableName;
    [SerializeField] protected AudioClip pickupSound;
    [SerializeField] protected GameObject pickupEffect;
    [SerializeField] protected bool destroyOnPickup = true;
    [SerializeField] protected string playerTag = "Player";
    [SerializeField] protected bool useCollisionTrigger = true;

    protected AudioSource audioSource;
    protected bool isPickable = true;
    protected Collider itemCollider;

    // Implémentation de la propriété de l'interface
    public string PickableName => pickableName;

    protected virtual void Awake()
    {
        InitializePickable();
    }

    // Méthode pour initialiser les composants nécessaires pour le ramassage
    protected virtual void InitializePickable()
    {
        // Initialiser l'AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // S'assurer qu'il y a un collider pour la détection
        itemCollider = GetComponent<Collider>();
        if (itemCollider == null)
        {
            // Ajouter un SphereCollider par défaut si aucun collider n'est présent
            itemCollider = gameObject.AddComponent<SphereCollider>();
            ((SphereCollider)itemCollider).radius = 0.5f; // Taille par défaut
        }

        // Configurer le collider comme trigger pour la détection automatique
        if (useCollisionTrigger)
        {
            itemCollider.isTrigger = true;
        }
    }

    // Implémentation de la méthode de l'interface
    public virtual bool CanBePickedUp(GameObject collector)
    {
        // Vérifiez si l'objet peut être ramassé
        if (!isPickable) return false;

        return true; // Par défaut, l'objet est toujours ramassable si isPickable est true
    }

    // Implémentation de la méthode de l'interface
    public virtual void OnPickup(GameObject collector)
    {
        // Jouer le son de ramassage
        if (pickupSound != null && audioSource != null)
            audioSource.PlayOneShot(pickupSound);

        // Créer un effet visuel si nécessaire
        if (pickupEffect != null)
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

        // Logique de ramassage spécifique à implémenter dans les classes enfants
        OnPickupEffect(collector);

        // Détruire l'objet après le ramassage si configuré ainsi
        if (destroyOnPickup)
            Destroy(gameObject);
    }

    // Méthode abstraite que les classes enfants doivent implémenter
    protected abstract void OnPickupEffect(GameObject collector);

    // Implémentation de la méthode de l'interface
    public virtual void OnPickupHover(GameObject collector)
    {
        // Par défaut ne fait rien, mais peut être surchargée par les classes enfants
    }

    // Méthode appelée automatiquement par Unity quand un autre collider entre dans le trigger
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (useCollisionTrigger && other.CompareTag(playerTag) && isPickable)
        {
            // Le joueur a touché l'item, déclencher le ramassage automatique
            if (CanBePickedUp(other.gameObject))
            {
                OnPickup(other.gameObject);
            }
        }
    }

    // Visualisation pour l'éditeur
    protected virtual void OnDrawGizmosSelected()
    {
        // Visualiser le collider en vert
        Gizmos.color = Color.green;

        // Si le collider existe déjà
        if (itemCollider != null)
        {
            // Différents types de visualisation selon le type de collider
            if (itemCollider is BoxCollider boxCollider)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            }
            else if (itemCollider is SphereCollider sphereCollider)
            {
                Gizmos.DrawWireSphere(transform.position + sphereCollider.center, sphereCollider.radius);
            }
            else if (itemCollider is CapsuleCollider capsuleCollider)
            {
                // Simplification pour la capsule
                Gizmos.DrawWireSphere(transform.position + capsuleCollider.center, capsuleCollider.radius);
            }
        }
    }
}