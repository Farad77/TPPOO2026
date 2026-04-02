using UnityEngine;

public class Motorcycle : Vehicle
{
    private float motorcycleLeanAngle;

    public override void Accelerate(float moveInput, float turnInput)
    {
        speed += acceleration * 1.2f * moveInput * Time.deltaTime; // Les motos accélèrent plus vite
                                                                   // Logique spécifique à la moto
        ApplyMotorcycleLean(turnInput);
    }

    public override void Brake()
    {
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
