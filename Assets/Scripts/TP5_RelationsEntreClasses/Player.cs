namespace TP5
{
    public class Player
    {
        public string name;
        public int health;
        public int maxHealth;

        // L'inventaire est directement intégré dans la classe Player
        public Inventory inventory = new Inventory();

        // Des références directes aux objets équipés
        public Item equippedWeapon;
        public Item equippedHelmet;
        public Item equippedChest;
        public Item equippedBoots;

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

        public void EquipArmor(Item armor)
        {
            if (armor.itemType == "Armor")
            {
                if (armor.armorType == "Helmet")
                {
                    equippedHelmet = armor;
                }
                else if (armor.armorType == "Chest")
                {
                    equippedChest = armor;
                }
                else if (armor.armorType == "Boots")
                {
                    equippedBoots = armor;
                }
            }
        }
    }
}