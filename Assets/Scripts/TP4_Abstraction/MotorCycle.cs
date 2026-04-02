using UnityEngine;

public class MotorCycle : Vehicule
{

    [SerializeField]
    private float motorcycleLeanAngle;
    void ApplyMotorcycleLean(float turnInput)
    {
        // Simuler l'inclinaison d'une moto dans les virages
        float targetLean = -turnInput * motorcycleLeanAngle;
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.z = Mathf.LerpAngle(currentRotation.z, targetLean, Time.deltaTime * 2.0f);
        transform.localEulerAngles = currentRotation;
    }

    public override void Accelerate(float moveInput, float turnInput)
    {
        speed += acceleration * 1.2f * moveInput * Time.deltaTime; // Les motos accélèrent plus vite
                                                                   // Logique spécifique à la moto
        ApplyMotorcycleLean(turnInput);
    }

    public override void Brake(float moveInput, float turnInput)
    {
        speed -= brakeForce * 0.8f * Mathf.Abs(moveInput) * Time.deltaTime;
    }

    public override void Steer(float moveInput, float turnInput)
    {
        // Logique de direction pour la moto
        transform.Rotate(0, turnInput * handling * speed * 0.15f * Time.deltaTime, 0);
    }


}
