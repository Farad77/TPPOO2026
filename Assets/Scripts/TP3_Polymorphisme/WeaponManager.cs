using System.Collections.Generic;
using UnityEngine;

namespace TP3_Polymorphisme
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField]
        private List<Weapon> weapons;

        private Weapon currentWeapon;

        private void Start()
        {
            if (weapons != null && weapons.Count > 0)
                currentWeapon = weapons[0];
        }

        public void Attack()
        {
            if (currentWeapon != null)
            {
                currentWeapon.Attack();
            }
            else
            {
                Debug.Log("No weapon equipped!");
            }
        }

        public void SwitchWeapon(int index)
        {
            currentWeapon = weapons[index];

            Debug.Log("Switched to " + currentWeapon.name);
        }

        public void NextWeapon()
        {
            int currentIndex = weapons.IndexOf(currentWeapon);
            int nextIndex = (currentIndex + 1) % weapons.Count;
            SwitchWeapon(nextIndex);
        }

        public void PreviousWeapon()
        {
            int currentIndex = weapons.IndexOf(currentWeapon);
            int previousIndex = (currentIndex - 1 + weapons.Count) % weapons.Count;
            SwitchWeapon(previousIndex);
        }
    }
}