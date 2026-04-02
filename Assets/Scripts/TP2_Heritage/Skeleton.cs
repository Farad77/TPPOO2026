using UnityEngine;

namespace TP2_Heritage
{
    public class Skeleton : Enemy
    {        
        
        void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Player")) {
                TP1_Encapsulation.Correction.PlayerCharacter player = collision.gameObject.GetComponent<TP1_Encapsulation.Correction.PlayerCharacter>();
                if (player != null) {
                    player.TakeDamage(damage);
                }
            }
        }
    }
}