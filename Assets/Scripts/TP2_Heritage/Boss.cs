using UnityEngine;

public class Boss : Enemy
{
    private Rigidbody rb;
    private float jumpForce = 20;
    [SerializeField]private bool isGrounded;

    protected override void Start()
    {
        base.Start();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();

        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo);
        if (Vector3.Distance(transform.position, hitInfo.point) < 1f) isGrounded = true;
        

        jump();
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

    private void jump()
    {
        if (isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isGrounded = false;
    }
}
