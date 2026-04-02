using UnityEngine;

public class AirPLane : Vehicule
{
    [SerializeField]
    private float airplaneLift;

    void ApplyAirplaneLift()
    {
        // Simuler la portance d'un avion
        if (Speed > MaxSpeed * 0.3f)
        {
            float liftForce = airplaneLift * (Speed / MaxSpeed);
            transform.Translate(Vector3.up * liftForce * Time.deltaTime, Space.World);
        }
    }

    public override void Accelerate(float moveInput, float turnInput)
    {
        Speed += Acceleration * 0.8f * moveInput * Time.deltaTime;
        // Logique spécifique à l'avion
        ApplyAirplaneLift();
    }

    public override void Brake(float moveInput, float turnInput)
    {
        Speed -= BrakeForce * 0.4f * Mathf.Abs(moveInput) * Time.deltaTime;
    }

    public override void Steer(float moveInput, float turnInput)
    {
        // Logique de direction pour l'avion
        transform.Rotate(turnInput * Handling * 0.5f * Time.deltaTime,
                        moveInput * Handling * 0.3f * Time.deltaTime,
                        -turnInput * Handling * Time.deltaTime);
    }


}
