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
                index = (value < 0) ? Storage.Count - 1 : (value >= Storage.Count) ? 0 : value;
            }
        }


        public Collectible GetItem() => (storage.Count > 0) ? storage[index] : null;


        public bool HasItem(Collectible item) => storage.Contains(item);


        public void AddToInventory(Collectible item) => storage.Add(item);


        public void ClearInventory() => storage.Clear();


        public void RemoveItem(Collectible item) 
        {
            int index = storage.IndexOf(item);
            Destroy(storage[index]);
            storage.RemoveAt(index);
        }


        public void RemoveItemNonDestructive(Collectible item)
        {
            storage.Remove(item);
        }


        public void Previous() => index--;


        public void Next() => index++;


        public float GetWeight()
        {
            float weight = 0f;
            for (int i = 0; i < storage.Count; i++)
            {
                weight += storage[i].Weight;
            }
            return weight;
        }
    }
}