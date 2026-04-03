using TP5;
using UnityEngine;

public class Armor : Item
{
    // Propriétés spécifiques aux armures
    public int defense { get; private set; }
    public string armorType { get; private set; } // "Helmet", "Chest", "Boots", etc.

    public override void UseItem<T>(T entity)
    {
        if (entity is Player player)
        {
            // Logique d'équipement d'une armure
            player.EquipArmor(this);
        }
        else
        {
            Debug.LogWarning("L'item ne peut être utilisé que par un Player !");
        }
    }
}
