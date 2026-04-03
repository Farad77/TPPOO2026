namespace TP5
{
    public class Inventory
    {
        public Item[] items = new Item[20]; // Taille fixe d'inventaire
        public int itemCount = 0;

        public void AddItem(Item item)
        {
            if (itemCount < items.Length)
            {
                items[itemCount] = item;
                itemCount++;
            }
        }

        public void RemoveItem(int index)
        {
            if (index >= 0 && index < itemCount)
            {
                // Décaler tous les éléments
                for (int i = index; i < itemCount - 1; i++)
                {
                    items[i] = items[i + 1];
                }
                items[itemCount - 1] = null;
                itemCount--;
            }
        }

        public float GetTotalWeight()
        {
            float totalWeight = 0;
            for (int i = 0; i < itemCount; i++)
            {
                totalWeight += items[i].weight;
            }
            return totalWeight;
        }
    }
}