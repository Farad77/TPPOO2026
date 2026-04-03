using UnityEngine;

namespace TP5
{
    public class Potion : Consumable
    {
        [SerializeField] private int healAmount;
        public int HealAmount
        {
            get => healAmount;
            private set { }
        }


        public override void ConsumeEffect()
        {
            if (Owner.TryGetComponent<LivingObject>(out LivingObject living))
            {
                living.Heal(HealAmount);
            }
        }
    }
}
