using UnityEngine;

namespace TP3_Polymorphisme.Correction
{
    /// <summary>
    /// Interface pour tous les objets qui peuvent recevoir des dégâts
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Méthode appelée quand l'objet reçoit des dégâts
        /// </summary>
        /// <param name="damage">Montant de dégâts à infliger</param>
        /// <returns>Vrai si l'objet a survécu, faux s'il est détruit</returns>
        bool TakeDamage(int damage);

        /// <summary>
        /// Propriété pour obtenir la santé actuelle de l'objet
        /// </summary>
        int CurrentHealth { get; }

        /// <summary>
        /// Propriété pour obtenir la santé maximale de l'objet
        /// </summary>
        int MaxHealth { get; }

        /// <summary>
        /// Propriété indiquant si l'objet est actuellement invulnérable
        /// </summary>
        bool IsInvulnerable { get; }
    }
}