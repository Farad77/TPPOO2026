using UnityEngine;

namespace TP5
{
    public class Sword : Weapon
    {

        public override void Use(Player player = null)
        {
            base.Use();
            Debug.Log("L'épée essaye de faire un truc... Mais riens ne se passe !");
        }


        public override void Attack()
        {
            Debug.Log("Attaque avec une épée pour " + Damage  + " dégâts!");
        }
    }
}

