using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace TP5
{
    public class Player : TP3_Polymorphisme.PlayerCharacter
    {

        // L'inventaire est directement intégré dans la classe Player
        private Inventory inventory;
        [SerializeField]
        private List<SlotArmor> slotArmors;

        private Item itemEquipped;

        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private GameObject hand;


        public Item ItemEquipped { get => itemEquipped; set => itemEquipped = value; }
        public GameObject Hand { get => hand; set => hand = value; }
        public List<SlotArmor> SlotArmors { get => slotArmors; set => slotArmors = value; }

        private void Start()
        {
            inventory = new Inventory(this);
        }

        public void ChangeItem()
        {
            inventory.NextItem();
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (itemImage != null)
            {
                if (itemEquipped != null)
                    itemImage.sprite = itemEquipped.ItemSprite;

                else
                {
                    itemImage.sprite = null;
                }
            }
        }

        public void UseItemEquipped()
        {
            if (itemEquipped != null)
            {
                if (itemEquipped is UsableItem)
                {
                    UsableItem item = itemEquipped as UsableItem;

                    item.Use(this);
                }
            }
        }
        public void AttackWithWeaponEquipped()
        {
            if (itemEquipped != null)
            {
                if (itemEquipped is Weapon)
                {
                    Weapon weapon = itemEquipped as Weapon;
                    weapon.Attack();
                }
            }
        }





        public void RestoreHealth(int amount)
        {
            Heal(System.Math.Min(Health + amount, MaxHealth));
            System.Console.WriteLine($"{PlayerName} restaure {amount} points de vie!");
        }

        public void EquipArmor(Armor armorToEquip)
        {
            inventory.EquipArmorToSlot(armorToEquip);
        }

        public void PickUpItem(Item item)
        {
            inventory.AddItemToInventory(item);
        }
    }



}