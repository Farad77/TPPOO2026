using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    [SerializeField]
    protected float speed = 10f;

    [SerializeField]
    protected float lifetime = 5f;

    [SerializeField]
    protected int damage = 10;

    [SerializeField]
    private Transform target;

    public Transform Target { get => target; set => target = value; }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }



    protected virtual void Update()
    {
        if (Target != null)
        {
            Vector3 direction = (Target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform == Target)
        {
            TP1_Encapsulation.Correction.PlayerCharacter player = other.gameObject.GetComponent<TP1_Encapsulation.Correction.PlayerCharacter>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }   




}
