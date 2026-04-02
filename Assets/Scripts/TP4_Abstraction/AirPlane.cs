using UnityEngine;

public class AirPLane : Vehicule
{
    [SerializeField]
    private float airplaneLift;

    void ApplyAirplaneLift()
    {
        // Simuler la portance d'un avion
        if (speed > maxSpeed * 0.3f)
        {
            float liftForce = airplaneLift * (speed / maxSpeed);
            transform.Translate(Vector3.up * liftForce * Time.deltaTime, Space.World);
        }
    }

    public override void Accelerate(float moveInput, float turnInput)
    {
        speed += acceleration * 0.8f * moveInput * Time.deltaTime;
        // Logique spécifique à l'avion
        ApplyAirplaneLift();
    }

    public override void Brake(float moveInput, float turnInput)
    {
        speed -= brakeForce * 0.4f * Mathf.Abs(moveInput) * Time.deltaTime;
    }

    public override void Steer(float moveInput, float turnInput)
    {
        // Logique de direction pour l'avion
        transform.Rotate(turnInput * handling * 0.5f * Time.deltaTime,
                        moveInput * handling * 0.3f * Time.deltaTime,
                        -turnInput * handling * Time.deltaTime);
    }


}
