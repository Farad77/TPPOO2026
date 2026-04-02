using UnityEngine;

namespace TP2_Heritage
{
    public class Skeleton : Enemy
    {
        //public int health = 80;
        //public int damage = 15;
        //public float speed = 3f;
        //public float detectionRange = 12f;
        //private Transform player;

        //void Start() {
        //    player = GameObject.FindGameObjectWithTag("Player").transform;
        //}

        //void Update() {
        //    if (Vector3.Distance(transform.position, player.position) < detectionRange) {
        //        Vector3 direction = (player.position - transform.position).normalized;
        //        transform.position += direction * speed * Time.deltaTime;
        //    }
        //}

        //public void TakeDamage(int amount) {
        //    health -= amount;
        //    if (health <= 0) {
        //        Die();
        //    }
        //}

        //private void Die() {
        //    Destroy(gameObject);
        //}

        //void OnCollisionEnter(Collision collision) {
        //    if (collision.gameObject.CompareTag("Player")) {
        //        TP1_Encapsulation.Correction.PlayerCharacter player = collision.gameObject.GetComponent<TP1_Encapsulation.Correction.PlayerCharacter>();
        //        if (player != null) {
        //            player.TakeDamage(damage);
        //        }
        //    }
        //}
    }
}