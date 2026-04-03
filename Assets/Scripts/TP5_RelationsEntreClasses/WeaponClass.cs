using UnityEngine;

namespace TP5
{
    public class WeaponClass : Item
    {

        [SerializeField] protected int damage;
        [SerializeField] protected float range;

        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void UseItem(Player player)
        {
            player.Attack(damage);
        }
    }

}
