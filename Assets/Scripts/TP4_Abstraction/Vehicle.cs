using UnityEngine;


[RequireComponent(typeof(VehicleController))]
public abstract class Vehicle : MonoBehaviour
{
    [Header("Vehicle Parameters")]    

    [Header("Acceleration & Speed")]


    [SerializeField] protected float maxSpeed;

    [SerializeField] protected float speed;

    protected float Speed
    {
        get => speed;
        set => speed = (value <= 0) ? 0 : (value >= maxSpeed) ? maxSpeed : value;
    }

    [SerializeField] protected float acceleration;

    [SerializeField] protected float baseAccelerationFactor;


    [Header("Braking")]


    [SerializeField] protected float handling;

    [SerializeField] protected float brakeForce;

    [SerializeField] protected float baseBrakeForceFactor;


    [Header("Drift")]


    [SerializeField] protected float baseRotationFactor;


    protected float lastMoveInput;
    protected float lastTurnInput;


    protected abstract void MovementComportment();


    protected abstract void Drift();


    protected virtual void Accelerate() => speed += acceleration * baseAccelerationFactor * lastMoveInput * Time.deltaTime;


    protected virtual void Brake() => speed -= brakeForce * baseBrakeForceFactor * Mathf.Abs(lastMoveInput) * Time.deltaTime;


    protected void Update() => transform.Translate(speed * Time.deltaTime * Vector3.forward);


    public void UpdateInputs(float moveInput, float turnInput)
    {
        lastMoveInput = moveInput;
        lastTurnInput = turnInput;
        switch (moveInput > 0)
        {
            case true:
                Accelerate();
                break;
            case false:
                Brake();
                break;
        }
        Drift();
    }
}
