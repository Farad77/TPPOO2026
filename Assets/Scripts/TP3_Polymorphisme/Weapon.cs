using System;
using UnityEngine;

namespace TP3_Polymorphisme_Nicolas
{
    public abstract class Weapon : MonoBehaviour
    {
        public string weaponName;

        public virtual void Attack()
        {


        }
    }

}