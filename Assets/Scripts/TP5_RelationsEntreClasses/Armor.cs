using UnityEngine;

namespace TP5
{
    public class Armor : Item
    {
        [SerializeField] protected int defense;
        [SerializeField] protected string armorType;

        protected virtual void UseItem(Player player)
        {
            player.EquipArmor(this);
        }
    }

}
