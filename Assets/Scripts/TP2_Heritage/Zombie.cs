using UnityEngine;

namespace TP2_Heritage
{
    public class Zombie : Enemy
    {


        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

        }
    }
}