using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject wand;

    [SerializeField] protected string currentWeapon = "sword";

    [SerializeField] protected int _damage;

    public virtual void Attack()
    {
        Debug.Log("Attack");
    }

    public void SwitchWeapon(string weaponName)
    {
        currentWeapon = weaponName;

        sword.SetActive(currentWeapon == "sword");
        bow.SetActive(currentWeapon == "bow");
        wand.SetActive(currentWeapon == "wand");

        Debug.Log("Switched to " + currentWeapon);
    }
}
