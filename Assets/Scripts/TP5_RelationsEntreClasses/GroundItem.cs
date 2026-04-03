using TP5;
using UnityEngine;

namespace TP5
{
    [RequireComponent(typeof(SphereCollider))]
    public class GroundItem : MonoBehaviour
    {
        public Item item;
        GameObject model;

        private void Start()
        {
           model = item.AppearObject(gameObject);

        }
        private void Update()
        {
            if (model != null)
            {
                // Rotationne l'objet en permanance
                model.transform.Rotate(Vector3.up, 50f * Time.deltaTime); // Rotation du collectible pour un effet visuel
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (item != null)
            {
                if(other.TryGetComponent<Player>(out Player player))
                {
                    player.PickUpItem(item);
                    item.DestroyItem();
                    Destroy(gameObject);
                }
            }
        }
    }
}
