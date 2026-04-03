using UnityEngine;

namespace TP5
{
    public class Armor : UsableItem
    {
        [SerializeField]
        private int defense;
        [SerializeField]
        private string slotNameToEquip;

        public string SlotNameToEquip { get => slotNameToEquip; set => slotNameToEquip = value; }

        protected int Defense { get => defense; set => defense = value; }

        public override void Use(Player player = null)
        {
            base.Use();
            player?.EquipArmor(this);
        }


    }
}

