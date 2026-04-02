using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Interface pour tous les objets ramassables
public interface IPickable
{
    // Propriété pour obtenir le nom de l'objet ramassable
    string PickableName { get; }

    // Méthode appelée quand l'objet est ramassé
    void OnPickup(GameObject collector);

    // Méthode pour vérifier si l'objet peut être ramassé
    bool CanBePickedUp(GameObject collector);

    // Méthode optionnelle pour effectuer des actions lorsque le joueur s'approche
    void OnPickupHover(GameObject collector);
}