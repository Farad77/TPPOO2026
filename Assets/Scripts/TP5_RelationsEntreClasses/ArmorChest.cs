using UnityEngine;
using TP5;
public class ArmorChest : Armor
{

    [SerializeField] private Item equippedChest;

    protected virtual void EquipArmor(Item armor)
    {
        equippedChest = armor;
    }
}
