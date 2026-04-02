using UnityEngine;

public class Car : Vehicle
{
    private float carTraction;

    public override void Accelerate(float moveInput, float turnInput)
    {
        speed += acceleration * moveInput * Time.deltaTime;
        // Logique spécifique à la voiture
        ApplyCarTraction();
    }

    public override void Brake()
    {
    }

    void ApplyCarTraction()
    {
        // Simuler la traction d'une voiture
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.0f))
        {
            float surfaceFactor = 1.0f;
            if (hit.collider.CompareTag("Dirt")) surfaceFactor = 0.7f;
            if (hit.collider.CompareTag("Ice")) surfaceFactor = 0.3f;
            speed *= (1.0f - (1.0f - carTraction) * (1.0f - surfaceFactor));
        }
    }
}
