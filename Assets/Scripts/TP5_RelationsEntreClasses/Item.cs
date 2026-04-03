
namespace TP5
{
    public abstract class Item
    {
        public string name { get; private set; }
        public string description { get; private set; }
        public float weight { get; private set; }
        public int value { get; private set; }

        public abstract void UseItem<T>(T entity);
    }
}