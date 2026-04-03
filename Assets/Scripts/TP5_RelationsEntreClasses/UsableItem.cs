using TP5;
using UnityEngine;

public abstract class UsableItem : Item, IUsable
{
    public virtual void Use(Player player = null)
    {
        Debug.Log($"{ItemName} est utilisé !");
    }
}
