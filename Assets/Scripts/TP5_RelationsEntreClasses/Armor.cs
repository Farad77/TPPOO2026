using TP5;
using UnityEngine;

public class Armor : Equipements
{
    private int defense;
    public enum armorTypes {Helmet, Chest, Boots};

    private armorTypes armorType;

    public int getDefense()
    {
        return defense;
    }
    
    public armorTypes GetArmorType()
    {
        return armorType;
    }


    public override void UseItem(Player player)
    {
        player.EquipArmor(this);
    }
}
