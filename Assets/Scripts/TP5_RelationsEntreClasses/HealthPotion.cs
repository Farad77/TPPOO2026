using TP5;
using UnityEngine;

public class HealthPotion : Potion
{
    [SerializeField]
    private int healingAmount;

    public override void Use(Player player = null)
    {
        // Logique d'utilisation d'une potion
        Debug.Log("You used a health potion and restored " + healingAmount + " health!");
        player.RestoreHealth(healingAmount);
    }

    public override void Throw()
    {
        // Logique de lancer d'une potion
        Debug.Log("You threw a health potion!");
        Destroy(gameObject, 1f); // Supposons que la potion est détruite après avoir été lancée
    }
}
