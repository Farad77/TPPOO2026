using UnityEngine;

namespace TP5
{
    public class Player : MonoBehaviour
    {
        public string name;
        public int health;
        public int maxHealth;

        // L'inventaire est directement intégré dans la classe Player
        public Inventory inventory = new Inventory();

        public void UseSelectedItem()
        {
            if (inventory.currentSelectedItem != null)
            {
                inventory.currentSelectedItem.UseItem(this);
            }
            else
            {
                System.Console.WriteLine("Aucun item sélectionné!");
            }
        }

        public void RestoreHealth(int amount)
        {
            health = System.Math.Min(health + amount, maxHealth);
            System.Console.WriteLine($"{name} restaure {amount} points de vie!");
        }

        public void EquipArmor(Armor armor)
        {
            inventory.EquipItem(armor);
        }
    }
}