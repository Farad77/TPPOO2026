public abstract class Weapon : Item, IWeapon
{
    protected int damage;
    protected int cooldownAmount;

    public override void Use()
    {
        Attack();
        Cooldown();
    }

    public abstract void Attack();
    public abstract void Cooldown();
}
