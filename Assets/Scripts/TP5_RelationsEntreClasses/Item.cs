
using System;
using UnityEngine;

namespace TP5
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private string itemName;

        [SerializeField]
        private string description;


        [SerializeField]
        private float weight;

        [SerializeField]
        private int value;

        [SerializeField]
        private string itemType;

        [SerializeField]
        private GameObject itemPrefab;
        [SerializeField]
        private Sprite itemSprite;

        private GameObject itemModelGenerated;
        private Transform parent;

        public string ItemName { get => itemName; set => itemName = value; }
        public string Description { get => description; set => description = value; }
        public float Weight { get => weight; set => weight = value; }
        public int Value { get => value; set => this.value = value; }
        public string ItemType { get => itemType; set => itemType = value; }
        public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
        public Sprite ItemSprite { get => itemSprite; set => itemSprite = value; }

        private void Update()
        {
            if(itemModelGenerated != null)
            {
                itemModelGenerated.transform.position = parent.transform.position;
            }
        }

        public void DestroyItem()
        {
            Destroy(itemModelGenerated);
            itemModelGenerated = null;
            parent = null;
        }

        public GameObject AppearObject(GameObject parentToAppear)
        {
            GameObject item = Instantiate(ItemPrefab, parentToAppear.transform.position, Quaternion.identity, parentToAppear.transform);
            itemModelGenerated = item;
            parent = parentToAppear.transform;
            return item;
        }
    }
}