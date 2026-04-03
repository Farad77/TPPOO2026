using UnityEngine;

namespace TP5
{
    public class Sword : Weapon
    {
        public override void InflictDamageOnTarget(GameObject target, int damage)
        {
            //To be adjusted for enemies
        }

        public override void Use()
        {
            Attack();
        }

        protected override bool Cooldown()
        {
            return true;
        }
    }
}
