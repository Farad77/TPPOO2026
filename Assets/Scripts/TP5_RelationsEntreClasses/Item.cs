
namespace TP5
{
    public class Item
    {
        public string name;
        public string description;
        public float weight;
        public int value;
        public string itemType; // "Weapon", "Potion", "Armor", etc.

        // Propriétés spécifiques aux armes
        public int damage;
        public float range;

        // Propriétés spécifiques aux potions
        public int healthRestored;
        public float duration;

        // Propriétés spécifiques aux armures
        public int defense;
        public string armorType; // "Helmet", "Chest", "Boots", etc.

        public void UseItem(Player player)
        {
            if (itemType == "Weapon")
            {
                // Logique d'utilisation d'une arme
                player.Attack(damage);
            }
            else if (itemType == "Potion")
            {
                // Logique d'utilisation d'une potion
                player.RestoreHealth(healthRestored);
            }
            else if (itemType == "Armor")
            {
                // Logique d'équipement d'une armure
                player.EquipArmor(this);
            }
        }
    }
}