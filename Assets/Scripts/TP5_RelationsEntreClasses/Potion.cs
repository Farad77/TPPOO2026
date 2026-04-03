using TP5;
using UnityEngine;

public abstract class Potion : UsableItem
{

    [SerializeField]
    protected int duration = 1;


    public override void Use(Player player = null)
    {
        base.Use(player);
    }

    public abstract void Throw();
}
