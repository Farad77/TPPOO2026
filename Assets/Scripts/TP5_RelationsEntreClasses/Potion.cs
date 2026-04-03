using TP5;
using UnityEngine;

public class Potion : Item
{

    [SerializeField] protected int healthRestored;
    [SerializeField] protected float duration;

    protected virtual void UseItem(Player player)
    {
        player.RestoreHealth(healthRestored);
    }
}
