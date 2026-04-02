using UnityEngine;

public class Airplane : Vehicle
{
    private float airplaneLift;

    public override void Accelerate(float moveInput, float turnInput)
    {
        speed += acceleration * 0.8f * moveInput * Time.deltaTime;
        // Logique spécifique à l'avion
        ApplyAirplaneLift();
    }

    public override void Brake()
    {
    }

    void ApplyAirplaneLift()
    {
        // Simuler la portance d'un avion
        if (speed > maxSpeed * 0.3f)
        {
            float liftForce = airplaneLift * (speed / maxSpeed);
            transform.Translate(Vector3.up * liftForce * Time.deltaTime, Space.World);
        }
    }
}
