using UnityEngine;

public class Moto : Vehicle
{
    [SerializeField] public float motorcycleLeanAngle;
    protected override void Move()
    {
        if (moveInput > 0)
        {
            speed += acceleration * 1.2f * moveInput * Time.deltaTime; // Les motos accélèrent plus vite
                                                                       // Logique spécifique à la moto
            ApplyMotorcycleLean(turnInput);
        }

        if (moveInput < 0)
        {
            speed -= brakeForce * 0.8f * Mathf.Abs(moveInput) * Time.deltaTime;
        }

        transform.Rotate(0, turnInput * handling * speed * 0.15f * Time.deltaTime, 0);
    }

    void ApplyMotorcycleLean(float turnInput)
    {
        // Simuler l'inclinaison d'une moto dans les virages
        float targetLean = -turnInput * motorcycleLeanAngle;
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.z = Mathf.LerpAngle(currentRotation.z, targetLean, Time.deltaTime * 2.0f);
        transform.localEulerAngles = currentRotation;
    }
}
