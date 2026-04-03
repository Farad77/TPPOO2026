using UnityEngine;

namespace TP5
{
    public class Bow : Weapon
    {

        public override void Use(Player player = null)
        {
            base.Use();
            Debug.Log("L'arc essaye de faire un truc... Mais riens ne se passe !");
        }


        public override void Attack()
        {
            Debug.Log("Flèche tirer ! " + Damage  + " dégâts infligé!");
        }
    }
}

