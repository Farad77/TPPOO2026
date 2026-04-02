using UnityEngine;

namespace TP3_Polymorphisme
{
    public class WeaponManager : MonoBehaviour
    {
        /*
        private Sword sword;
        private Bow bow;
        private Wand wand;
        private Knife knife;
        private string currentWeapon = "sword";*/

        private Weapon currentWeapon;
        
        public void Attack()
        {
            /*
            if (currentWeapon == "sword")
            {
                sword.Attck();
            }
            else if (currentWeapon == "bow")
            {
                bow.Attck();
            }
            else if (currentWeapon == "wand")
            {
                wand.Attck();
            }
            else if (currentWeapon == "knife")
            {
                knife.Attck();
            }*/

            currentWeapon.Attck();
        }
        
        public void SwitchWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Debug.Log("Switched to " + currentWeapon);
        }
    }
}