using UnityEngine;
using TP5;

public class ArmorHelmet : Armor
{
    [SerializeField] private Item equippedHelmet;

    protected virtual void EquipArmor(Item armor)
    {
        equippedHelmet = armor;
    }
}
