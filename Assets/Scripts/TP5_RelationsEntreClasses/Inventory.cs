using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TP5
{
    public class Inventory
    {
        private List<SlotArmor> slotArmors;
        private List<Item> items;
        public List<Item> Items { get => items; set => items = value; }

        private int actualItemIndex = 0;

        private Player player;

        public Inventory(Player player)
        {
            this.slotArmors = player.SlotArmors;
            this.player = player;
            Items = new List<Item>();
        }

        public void AddItemToInventory(Item item)
        {
            Debug.Log(item.ItemName + " rajouté à l'inventaire");
            Items.Add(item);
        }

        public void RemoveItemFromInventory(Item item)
        {
            Items.Remove(item);
        }

        public void EquipArmorToSlot(Armor armor)
        {
            foreach (SlotArmor slotArmor in slotArmors)
            {
                if (slotArmor.SlotName == armor.SlotNameToEquip)
                {
                    if (slotArmor.ArmorEquipped != null && slotArmor.ArmorEquipped != armor)
                    {
                        UnequipArmorToSlot(slotArmor);
                    }
                    player.ChangeItem();
                    slotArmor.EquipArmor(armor);
                    RemoveItemFromInventory(armor);
                }
            }
        }

        public void UnequipArmorToSlot(SlotArmor slotArmor)
        {
            Armor armor = slotArmor.UnequipArmor();
            AddItemToInventory(armor);
        }

        public Item GetItemByIndex(int index)
        {
            return Items[index];
        }

        public void RemoveItem(int index)
        {
            Items.RemoveAt(index);
        }

        public float GetTotalWeight()
        {
            float totalWeight = 0;
            foreach (Item item in Items)
            {
                totalWeight += item.Weight;
            }
            return totalWeight;
        }

        public void NextItem()
        {
            if (items.Count == 0)
                return;
            Debug.Log("Switching item...");
            actualItemIndex = (actualItemIndex + 1) % items.Count;
            SwitchItem(items[actualItemIndex]);
        }


        public void SwitchItem(Item item)
        {
            if (player.ItemEquipped != null)
            {
                player.ItemEquipped.DestroyItem();
                player.ItemEquipped = null;
            }

            if(item == null || items.Count < 1)
            {
                return;
            }

            // Trouve l'item dans l'inventaire et l'affiche
            foreach (var i in items)
            {
                if (i == item)
                {
                    item.AppearObject(player.Hand);
                    player.ItemEquipped = item;
                    break;
                }
            }

        }
    }

}

