using System.Collections.Generic;
using UnityEngine;

namespace TP5
{
    public class Inventory : MonoBehaviour
    {
        private List<Collectible> storage;
        public List<Collectible> Storage { get => storage ??= new(); private set { } }


        private int index;
        public int Index
        {
            get => index;
            private set
            {
                index = (value < 0) ? (Storage.Count - 1 < 0) ? 0 : Storage.Count : (value >= Storage.Count) ? 0 : value;
            }
        }


        public Collectible GetItem()
        {
            if (Storage.Count == 0) return null;
            return Storage[index];
        }


        public bool HasItem(Collectible item) => Storage.Contains(item);


        public void AddToInventory(Collectible item) => Storage.Add(item);


        public void ClearInventory() => Storage.Clear();


        public void RemoveItem(Collectible item) 
        {
            int index = Storage.IndexOf(item);
            Destroy(storage[index]);
            Storage.RemoveAt(index);
        }


        public void RemoveItemNonDestructive(Collectible item)
        {
            Storage.Remove(item);
        }


        public void Previous() => index--;


        public void Next() => index++;


        public float GetWeight()
        {
            float weight = 0f;
            for (int i = 0; i < Storage.Count; i++)
            {
                weight += Storage[i].Weight;
            }
            return weight;
        }
    }
}