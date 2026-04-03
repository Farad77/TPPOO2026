using TP5;
using UnityEngine;

public class Weapon : Item
{
    // Propriétés spécifiques aux armes
    public int damage;
    public float range;

    public override void UseItem<T>(T entity)
    {
        if (entity is Player player)
        {
            // Logique d'attaque avec l'arme équipée
            System.Console.WriteLine($"{name} attaque pour {damage} points de dégâts!");
        }
        else
        {
            Debug.LogWarning("L'item ne peut être utilisé que par un Player !");
        }
    }
}
