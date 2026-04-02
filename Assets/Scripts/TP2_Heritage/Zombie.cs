using UnityEngine;

namespace TP2_Heritage
{
    public class Zombie : MonoBehaviour
    {
        public int health = 100;
        public int damage = 10;
        public float speed = 2f;
        public float detectionRange = 10f;
        private Transform player;
        
        void Start() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        void Update() {
            if (Vector3.Distance(transform.position, player.position) < detectionRange) {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
        
        public void TakeDamage(int amount) {
            health -= amount;
            if (health <= 0) {
                Die();
            }
        }
        
        private void Die() {
            Destroy(gameObject);
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