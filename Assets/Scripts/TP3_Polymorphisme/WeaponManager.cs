using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TP3_Polymorphisme_Nicolas;
using UnityEngine;

namespace TP3_Polymorphisme
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField]
        List<Weapon> weapons;

        [SerializeField]
        private Weapon currentWeapon;

        public int actualWeaponIndex = 0;

        [SerializeField]
        private Transform handPlayer;


        public void Attack()
        {
            if (currentWeapon != null)
                currentWeapon.Attack();
        }

        public void NextWeapon()
        {
            if (weapons.Count == 0)
                return;
            Debug.Log("Switching weapon...");
            actualWeaponIndex = (actualWeaponIndex + 1) % weapons.Count;
            SwitchWeapon(weapons[actualWeaponIndex]);
        }


        public void SwitchWeapon(Weapon weapon)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
            }

            // Trouve l'arme dans la liste et l'affiche
            foreach (var w in weapons)
            {
                if (w.weaponName == weapon.weaponName)
                {
                    Weapon tempWeapon = Instantiate(w, handPlayer.position, Quaternion.identity);
                    tempWeapon.transform.SetParent(transform);
                    currentWeapon = tempWeapon;
                }
            }

        }
    }
}