using UnityEngine;

namespace TP5
{
    public abstract class Weapon: Item, IDamager
    {
        [SerializeField] private int damage;
        public int Damage
        {
            get => damage;
            private set { }
        }

        protected virtual bool CanAttack() => Cooldown();
        protected abstract bool Cooldown();
        protected virtual void Attack()
        {
            if (!CanAttack()) return;
            System.Console.WriteLine($"{OwnerName} attaque pour {Damage} points de dégâts!");
            // CODE TO CHECK IF ENEMY AROUND TO CALL INFLICT DAMAGE ON TARGET. MEANT TO BE OVERRIDEN.
        }

        public abstract void InflictDamageOnTarget(GameObject target, int damage);
    }
}