using UnityEngine;

namespace TP2_Heritage
{
    public class Skeleton : Enemy
    {
        protected override void Update()
        {
            base.Update();
        }
        protected override void TakeDamage(int amount)
        {
            base.TakeDamage(1);
        }

        protected override void Die()
        {
            Destroy(gameObject);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
        }
    }
}