using UnityEngine;

namespace TP5
{
    public abstract class Collectible : MonoBehaviour, ICollectable
    {
        private GameObject owner;
        public GameObject Owner
        {
            get => owner;
            set 
            {
                ownerName = GetOwnerName();
                owner = value; 
            }
        }


        private string ownerName;
        public string OwnerName
        {
            get => ownerName;
            private set => ownerName = value;
        }


        private string GetOwnerName()
        { 
            if (owner.TryGetComponent<LivingObject>(out LivingObject livingObject)) return livingObject.LivingName;
            return "";
        }


        [SerializeField] private int weight;
        public int Weight { get => weight; private set { } }


        [SerializeField] private int itemName;
        public int ItemName { get => itemName; private set { } }


        [SerializeField] private int description;
        public int Description { get => description; private set { } }


        public GameObject Collect()
        {
            return Instantiate(this, transform.position, Quaternion.identity).gameObject;
        }

        /*

        // Propriétés spécifiques aux armes
        public int damage;
        public float range;

        // Propriétés spécifiques aux potions
        public int healthRestored;
        public float duration;

        // Propriétés spécifiques aux armures
        public int defense;

        
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
           
        */
    }
}