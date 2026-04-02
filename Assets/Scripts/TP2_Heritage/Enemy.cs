using TP1_Encapsulation.Correction;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    public Enemy(int Health, int Damage, float Speed, float DetectionRange)
    {
        health = Health;
        damage = Damage;
        speed = Speed;
        detectionRange = DetectionRange;
    }

    [Header("Enemy")]

    [Header("Health")]
    // Health Variables
    [SerializeField] private int maxHealth;
    public int MaxHealth 
    { 
        get => maxHealth; 
        set => maxHealth = (value <= 0) ? 0 : value; 
    }

    private int health;
    public int Health
    { 
        get => health; 
        set 
        { 
            health = (value <= 0) ? 0 : (value >= maxHealth) ? maxHealth : value; 
            if (health == 0) Death(); 
        } 
    }


    [Header("Damage")]
    // Damage Variables

    [SerializeField] private int damage;
    public int Damage 
    { 
        get => damage; 
        set => damage = (value <= 0) ? 0 : value; 
    }


    [Header("Movement")]
    // Movement Variables

    [SerializeField] private float maxSpeed;
    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = (value <= 0) ? 0 : value;
    }

    [SerializeField] private float speed;
    public float Speed
    {
        get => speed;
        set => speed = (value <= 0) ? 0 : (value >= maxSpeed) ? maxSpeed : value;
    }


    [Header("DetectionRange")]
    // Detection Variables
    [SerializeField] private float detectionRange;
    public float DetectionRange
    {
        get => detectionRange;
        set => detectionRange = (value <= 0) ? 0 : value;
    }


    protected Transform player;


    // Behaviour Variables
    protected delegate void Behaviors();
    protected Behaviors enemyBehavior;


    // State Variables
    protected enum EnemyState { BASE_STATE, DEATH_STATE, ATTACK_STATE, CHASE_STATE }
    protected EnemyState currentState = EnemyState.BASE_STATE;


    protected virtual void Start()
    {
        SetStats(maxHealth, damage, speed, detectionRange);
        player = FindFirstObjectByType<PlayerCharacter>().transform;
    }


    protected virtual void Update() => ExecBehaviour();


    protected virtual void ExecBehaviour() => enemyBehavior?.Invoke();


    /// <summary>
    /// Base virtual function needs to be called first.
    /// </summary>
    protected virtual void SetupBehaviour()
    {
        ClearBehaviour();
        enemyBehavior += BaseBehaviour;
    }


    protected void ClearBehaviour() => enemyBehavior = null;


    protected void OnDisable() => ClearBehaviour();


    protected void OnEnable() => SetupBehaviour();


    protected virtual void UpdateBehaviours() => SetupBehaviour();


    // Base Behaviours for enemies


    protected virtual void BaseBehaviour()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;
        }
    }


    protected void SetStats(int Health, int Damage, float Speed, float DetectionRange)
    {
        health = Health;
        damage = Damage;
        speed = Speed;
        detectionRange = DetectionRange;
    }


    protected virtual void Death() => Destroy(gameObject);


    public void TakeDamage(int amount) => health -= amount;


    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<PlayerCharacter>(out PlayerCharacter player))
            {
                player.TakeDamage(damage);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        print("Health : " + health.ToString() + " ; " + "Damage : " + damage.ToString() + " ; " + "Speed : " + speed.ToString() + " ; " + "Max Speed : " + maxSpeed.ToString() + " ; " + "Detection Range : " + detectionRange.ToString());
    }
}
