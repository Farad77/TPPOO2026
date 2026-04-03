namespace TP5
{
    public class Player
    {
        public string name;
        public int health;
        public int maxHealth;

        // L'inventaire est directement intégré dans la classe Player
        private Inventory inventory = new Inventory();

        // Des références directes aux objets équipés
        public Item equippedWeapon;
        public Item equippedHelmet;
        public Item equippedChest;
        public Item equippedBoots;

        // Getter et Setter
        public string getName()
        {
            return name;
        }

        public float getHealth()
        {
            return health;
        }

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

        public void EquipArmor(Armor armor)
        {

        }
    }
}