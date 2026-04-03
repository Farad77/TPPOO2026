using TP5;
using UnityEngine;

public class Potion : Item
{
    // Propriétés spécifiques aux potions
    public int healthRestored;
    public float duration;

    public override void UseItem<T>(T entity)
    {
        if (entity is Player player)
        {
            // Logique d'utilisation d'une potion
            player.RestoreHealth(healthRestored);
        }
        else
        {
            Debug.LogWarning("L'item ne peut être utilisé que par un Player !");
        }

    }
}
