using TP1_Encapsulation.Correction;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health = 80;
    [SerializeField] protected int damage = 15;
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float detectionRange = 12f;
    protected Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    protected  virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    protected virtual void Move()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
