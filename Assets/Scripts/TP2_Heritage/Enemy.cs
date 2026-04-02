using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float detectionRange;
    protected Transform player;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (player.gameObject.TryGetComponent<TP1_Encapsulation.Correction.PlayerCharacter>(out var playerCharacter))
        {
            if (!playerCharacter.IsDead)
            {
                if (Vector3.Distance(transform.position, player.position) < detectionRange)
                {

                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
            }
        }

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TP1_Encapsulation.Correction.PlayerCharacter player = collision.gameObject.GetComponent<TP1_Encapsulation.Correction.PlayerCharacter>();
            if (player != null)
            {
                if (player.IsInvincible || player.IsDead)
                    return;

                player.TakeDamage(damage);
            }
        }
    }

}
