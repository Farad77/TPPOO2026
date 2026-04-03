using System.Collections.Generic;

namespace TP5
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();
        private int itemCount = 0;

        // Getter
        public List<Item> getItems()
        {
            return items;
        }

        public int getItemCount()
        {
            return itemCount;
        }

        // Méthode spécifique
        public void AddItem(Item item)
        {
            items.Add(item);
            itemCount++;
        }

        public void RemoveItem(int index)
        {
            items.RemoveAt(index);
            itemCount--;
        }

        public float GetTotalWeight()
        {
            float totalWeight = 0;
            for (int i = 0; i < itemCount; i++)
            {
                totalWeight += items[i].getWeight();
            }
            return totalWeight;
        }
    }
}