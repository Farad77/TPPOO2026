using System.Collections.Generic;
using UnityEngine;

namespace TP3_Polymorphisme
{
    public class WeaponManager : MonoBehaviour
    {
        public List<Item> items = new();
        
        private int currentItem;
        public int CurrentItem
        {
            get => currentItem;
            set => currentItem = (value > items.Count) ? 0 : (value <= 0) ? items.Count : value;
        }
                
        public void NextItem() => currentItem++;
        public void PreviousItem() => currentItem--;

                
        public void Use()
        {
            if (items.Count != 0)
            {
                if (items[currentItem]) items[currentItem].Use();
            }
        }
    }
}