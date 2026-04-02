using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float acceleration;
    [SerializeField] protected float handling;
    [SerializeField] protected float brakeForce;

    protected float moveInput; 
    protected float turnInput; 


    protected void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        Move();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    protected abstract void Move();

}
