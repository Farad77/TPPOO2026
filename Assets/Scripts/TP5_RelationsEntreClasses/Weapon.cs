using UnityEngine;

namespace TP5
{
    public abstract class Weapon : UsableItem
    {
        // Propriétés spécifiques aux armes
        private int damage;
        private float range;

        protected int Damage { get => damage; set => damage = value; }
        protected float Range { get => range; set => range = value; }

        public override void Use(Player player = null)
        {
            base.Use();
        }

        public abstract void Attack();
    }
}

