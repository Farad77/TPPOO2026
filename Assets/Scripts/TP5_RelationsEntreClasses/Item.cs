
namespace TP5
{
    public abstract class Item
    {
        private string name;
        private string description;
        private float weight;
        private int value;

        public string getName()
        {
            return name;
        }

        public string getDescription()
        {
            return description;
        }

        public float getWeight()
        {
            return weight;
        }

        public int getValue()
        {
            return value;
        }

        public virtual void UseItem(Player player)
        {
        }
    }
}