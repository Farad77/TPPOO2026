using TP5;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : LivingObject, IMovement
{
    private Inventory inventory;
    private Inventory equipment;

    private void Start() 
    {
        inventory = this.AddComponent<Inventory>();
        equipment = this.AddComponent<Inventory>();
    }


    private void Update() => InputManager();


    private void OnNext() => inventory.Next();
    private void OnPrevious() => inventory.Previous();


    private void OnUse() 
    {
        if (inventory.GetItem() == null) return;
        Collectible currentItem = inventory.GetItem();
        if (currentItem == null) return;
        if (currentItem.TryGetComponent<IUsable>(out IUsable item))
        {
            item.Use();
        }
        else if (currentItem.TryGetComponent<IEquipable>(out IEquipable equip))
        {
            if (equipment.HasItem(currentItem))
            {
                equipment.RemoveItemNonDestructive(currentItem);
                equip.DeactivatePassiveEffects();
            }
            else
            {
                equipment.AddToInventory(currentItem);
                equip.ActivatePassiveEffects();
            }
        }
    }


    private void OnPickUp() 
    {
        Collider[] colliders = new Collider[100];
        if (Physics.OverlapSphereNonAlloc(transform.position, 2.5f, colliders) > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (!collider) continue;
                if (!collider.TryGetComponent<ICollectable>(out ICollectable interfaceCollectable)) continue;
                GameObject item = interfaceCollectable.Collect();
                if (item.TryGetComponent<Collectible>(out Collectible collectible))
                {                   
                    inventory.AddToInventory(collectible);
                    collectible.transform.parent = transform;
                    collectible.Owner = this.gameObject;
                    Destroy(collider.gameObject);
                }
                else Destroy(item);
            }
        }
    }

    public void Move(float AxisX, float AxisY)
    {
        transform.position += AxisX * Time.deltaTime * transform.right + AxisY * Time.deltaTime * transform.forward;
    }


    private void InputManager()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Move(moveX, moveY);

        if (Input.GetButtonDown("Fire1")) OnUse();
        if (Input.GetButtonDown("Next")) OnNext();
        if (Input.GetButtonDown("Previous")) OnPrevious();
        if (Input.GetButtonDown("Fire2")) OnPickUp();        
    }
}
