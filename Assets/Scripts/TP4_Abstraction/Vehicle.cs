using JetBrains.Annotations;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    protected float speed;
    protected float maxSpeed;
    protected float acceleration;
    protected float handling;
    protected float brakeForce;

    public abstract void Accelerate(float moveInput, float turnInput);
    public abstract void Brake();
}
