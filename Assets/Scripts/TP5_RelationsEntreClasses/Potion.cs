using TP5;
using UnityEngine;

public class Potion : Item
{
    private int healthRestored;
    private float duration;

    // getters
    public int getHealthRestored()
    {
        return healthRestored;
    }

    public float getDuration()
    {
        return duration;
    }

    // Methods
    public override void UseItem(Player player)
    {
        // Logique d'utilisation d'une potion
        player.RestoreHealth(healthRestored);
    }
}
