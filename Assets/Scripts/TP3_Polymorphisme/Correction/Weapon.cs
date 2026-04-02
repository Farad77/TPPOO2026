using UnityEngine;

public abstract class Weapon : PickableMonoBehaviour
{
    [Header("Weapon Settings")]
    private string weaponName;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRate;
    [SerializeField] protected AudioClip attackSound;

    // Le pickableName de la classe parente est utilisé comme weaponName
    
    public int Damage => damage;

    public string WeaponName { get => weaponName; set => weaponName = value; }

    protected float lastAttackTime;

    protected override void Awake()
    {
        // Appeler la méthode Awake de la classe parente
        base.Awake();

        // Autres initialisations spécifiques aux armes
    }

    // Méthode abstraite que chaque arme doit implémenter
    public abstract void Attack();

    // Vérifie si l'arme peut attaquer (basé sur le taux d'attaque)
    public virtual bool CanAttack()
    {
        if (Time.time - lastAttackTime >= 1f / attackRate)
        {
            lastAttackTime = Time.time;
            return true;
        }
        return false;
    }

    // Méthode commune pour jouer un son d'attaque
    protected void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
            audioSource.PlayOneShot(attackSound);
    }

    // Méthode virtuelle pour les effets visuels
    protected virtual void CreateAttackEffect()
    {
        // Implémentation de base, peut être surchargée
    }

    // Implémenter la méthode abstraite de PickableMonoBehaviour
    protected override void OnPickupEffect(GameObject collector)
    {
        // Chercher le WeaponManager sur le joueur
        TP3_Polymorphisme.Correction.WeaponManager weaponManager = collector.GetComponent<TP3_Polymorphisme.Correction.WeaponManager>();

        if (weaponManager != null)
        {
            // Ajouter cette arme au WeaponManager
            // Puisque l'arme est déjà créée (c'est this), pas besoin d'en créer une nouvelle

            // D'abord désactiver l'arme car le WeaponManager gère l'activation
            gameObject.SetActive(false);

            // Détacher l'arme de sa position actuelle et l'attacher au joueur
            transform.SetParent(weaponManager.transform);
            transform.localPosition = Vector3.zero+Vector3.up;
            transform.localRotation = Quaternion.identity;

            // Ajouter l'arme au manager
            weaponManager.AddWeapon(this, true);

            Debug.Log($"Le joueur a ramassé l'arme: {pickableName}");
        }
        else
        {
            Debug.LogWarning("Le joueur n'a pas de WeaponManager.");
        }
    }

    // Surcharger OnDrawGizmosSelected pour inclure aussi l'affichage de la classe parente
    protected override void OnDrawGizmosSelected()
    {
        // Appeler la méthode de la classe parente pour afficher le collider
        base.OnDrawGizmosSelected();

        // Ajoutez ici toute visualisation supplémentaire pour l'arme
    }
}