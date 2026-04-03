namespace TP5
{
    public abstract class Consumable : Item, IConsumable
    {
        public abstract void ConsumeEffect();

        public override void Use()
        {
            ConsumeEffect();
            DestroyItem();
        }
    }
}
