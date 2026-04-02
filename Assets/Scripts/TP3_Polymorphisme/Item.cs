using TP3_Polymorphisme;
using UnityEngine;

public abstract class Item : MonoBehaviour, IUsable
{
    public abstract void Use();
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<WeaponManager>(out WeaponManager manager))
            {
                manager.items.Add(Instantiate(this, manager.transform.position, manager.transform.rotation, manager.transform));
                Destroy(this.gameObject);
            }
        }
    }
}
