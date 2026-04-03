using TP5;
using UnityEngine;

public class TP5_Weapon : Equipement
{
    private int damage;
    private float range;

    // Getter
    public int getDamage() {
        return damage;
    }

    public float getRange()
    {
        return range;
    }

    // Méthode spécifique
    public override void UseItem(Player player)
    {
        player.EquipItem(this);
    }
}
