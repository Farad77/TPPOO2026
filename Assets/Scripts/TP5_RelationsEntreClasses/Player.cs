using Unity.VisualScripting;

namespace TP5
{
    public class Player
    {
        private string name;
        private int health;
        private int maxHealth;

        // L'inventaire est directement intégré dans la classe Player
        private Inventory inventory = new Inventory();

        private PlayerEquipements equipements = new PlayerEquipements();

        // Getter et Setter
        public string getName()
        {
            return name;
        }

        public float getHealth()
        {
            return health;
        }

        public float getMaxHealth()
        {
            return maxHealth;
        }

        public PlayerEquipements getEquipements()
        {
            return equipements;
        }

        public Inventory GetInventory()
        {
            return inventory;
        }



        // Méthodes spécifiques
        public void Attack(int damage)
        {
            // Logique d'attaque avec l'arme équipée
            System.Console.WriteLine($"{name} attaque pour {damage} points de dégâts!");
        }

        public void RestoreHealth(int amount)
        {
            health = System.Math.Min(health + amount, maxHealth);
            System.Console.WriteLine($"{name} restaure {amount} points de vie!");
        }

        public void LoseHealth(int amount)
        {
            health = System.Math.Max(health - amount, 0);
            System.Console.WriteLine($"{name} perd {amount} points de vie!");
        }

        public void UseItem(Item item)
        {
            item.UseItem(this);
        }

        public void EquipItem(Equipement equipement)
        {
            equipements.EquipItem(equipement);
        }
    }
}