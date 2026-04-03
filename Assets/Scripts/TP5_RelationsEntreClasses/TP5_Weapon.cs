using TP5;
using UnityEngine;

public class TP5_Weapon : Equipements
{
    private int damage;
    private float range;

    public int getDamage() {
        return damage;
    }

    public float getRange()
    {
        return range;
    }

    public override void UseItem(Player player)
    {
        base.UseItem(player);
    }
}
