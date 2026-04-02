using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TP2_Heritage
{
    public class Zombie : Enemy
    {

        private void Start()
        {

        }
        void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Player")) {
                PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();   
                if (player != null) {
                    player.TakeDamage(damage);
                }
            }
        }
    }
}