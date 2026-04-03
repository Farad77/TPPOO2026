using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TP5
{
    [Serializable]
    public class Inventory
    {
        private List<Item> items = new();
        private int itemCount = 0;

        private int currentItemIndex = 0;

        public Item currentSelectedItem => items.Count > 0 ? items[currentItemIndex] : null;

        public Item equippedWeapon { get; private set; }
        public Item equippedHelmet { get; private set; }
        public Item equippedChest { get; private set; }
        public Item equippedBoots { get; private set; }

        #region Gestion des items

        /// <summary>
        /// Cette méthode permet d'ajouter un item à l'inventaire.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            if (items != null && item != null)
            {
                items.Add(item);
                itemCount++;
            }
        }

        /// <summary>
        /// Cette méthode permet de supprimer un item de l'inventaire en fonction de son index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItem(int index)
        {
            if (items != null && index >= 0 && index < itemCount)
            {
                items.RemoveAt(index);
                itemCount--;
            }
        }

        #endregion

        /// <summary>
        /// Cette méthode permet de calculer le poids total de tous les items présents dans l'inventaire.
        /// </summary>
        /// <returns></returns>
        public float GetTotalWeight()
        {
            if (items == null || itemCount == 0)
                return 0;

            float totalWeight = 0;
            foreach (Item item in items)
            {
                totalWeight += item.weight;
            }

            return totalWeight;
        }

        /// <summary>
        /// Cette méthode permet de récupérer l'item actuellement sélectionné dans l'inventaire.
        /// </summary>
        /// <returns></returns>
        public Item GetSelectedItem()
        {
            if (items != null && itemCount > 0)
            {
                return items[currentItemIndex];
            }

            return null;
        }

        public void EquipItem(Item item)
        {
            if (item is Weapon weapon)
            {
                equippedWeapon = weapon;
            }
            else if (item is Armor armor)
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

        #region Navigation dans l'inventaire

        /// <summary>
        /// Cette méthode permet de sélectionner l'item suivant dans l'inventaire. Si on est à la fin de la liste, elle revient au début.
        /// </summary>
        public void SelectNextItem()
        {
            if (itemCount > 0)
            {
                currentItemIndex = (currentItemIndex + 1) % itemCount;
                Debug.WriteLine($"Item sélectionné : {currentSelectedItem.name}");
            }
        }

        /// <summary>
        /// Cette méthode permet de sélectionner l'item précédent dans l'inventaire. Si on est au début de la liste, elle revient à la fin.
        /// </summary>
        public void SelectPreviousItem()
        {
            if (itemCount > 0)
            {
                currentItemIndex = (currentItemIndex - 1 + itemCount) % itemCount;
                Debug.WriteLine($"Item sélectionné : {currentSelectedItem.name}");
            }
        }

        #endregion
    }
}