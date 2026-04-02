using System;
using UnityEngine;

public abstract class Vehicule : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float maxSpeed;


    [SerializeField]
    protected float acceleration;


    [SerializeField]
    protected float handling;

    [SerializeField]
    protected float brakeForce;

    public float Speed { get => speed; set => speed = Mathf.Clamp(value, 0, MaxSpeed);}
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }
    public float Handling { get => handling; set => handling = value; }
    public float BrakeForce { get => brakeForce; set => brakeForce = value; }


    public abstract void Accelerate(float moveInput, float turnInput);
    public abstract void Brake(float moveInput, float turnInput);
    public abstract void Steer(float moveInput, float turnInput);

    public void Move(float moveInput, float turnInput)
    {
        // Gérer l'accélération et le freinage
        if (moveInput > 0)
        {
            // Accélération
            Accelerate(moveInput, turnInput);
        }
        else if (moveInput < 0)
        {
            // Freinage
            Brake(moveInput, turnInput);
        }

        // Gérer la direction

        Steer(moveInput, turnInput);

        // Déplacement
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
