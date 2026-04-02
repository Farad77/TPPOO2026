using UnityEngine;

namespace TP3_Polymorphisme
{
    public abstract class Weapon : MonoBehaviour
    {
        public string weaponName;
        public int damage;

        public abstract void Attack();
    }
}