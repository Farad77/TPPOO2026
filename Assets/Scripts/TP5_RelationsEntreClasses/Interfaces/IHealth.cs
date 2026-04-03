namespace TP5
{
    public interface IHealth
    {
        public void Heal(int amount);
        public void Damage(int amount);
        public void Death();
    }
}