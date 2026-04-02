using UnityEngine;

public class Avion : Vehicle
{
    [SerializeField] private float airplaneLift;
    protected override void Move()
    {
        if (moveInput > 0)
        {
            speed += acceleration * 0.8f * moveInput * Time.deltaTime;
            // Logique spécifique à l'avion
            ApplyAirplaneLift();
        }

        if (moveInput < 0)
        {
            speed -= brakeForce * 0.4f * Mathf.Abs(moveInput) * Time.deltaTime;
        }
        //gerer la direction
        transform.Rotate(turnInput * handling * 0.5f * Time.deltaTime,
                            moveInput * handling * 0.3f * Time.deltaTime,
                            -turnInput * handling * Time.deltaTime);
    }

    private void ApplyAirplaneLift()
    {
        // Simuler la portance d'un avion
        if (speed > maxSpeed * 0.3f)
        {
            float liftForce = airplaneLift * (speed / maxSpeed);
            transform.Translate(Vector3.up * liftForce * Time.deltaTime, Space.World);
        }
    }
}
