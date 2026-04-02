using UnityEngine;

public class Car : Vehicule
{
    [SerializeField]
    private float carTraction;

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

    public override void Accelerate(float moveInput, float turnInput)
    {
        speed += acceleration * moveInput * Time.deltaTime;
        // Logique spécifique à la voiture
        ApplyCarTraction();
    }

    public override void Brake(float moveInput, float turnInput)
    {
        speed -= brakeForce * Mathf.Abs(moveInput) * Time.deltaTime;
    }

    public override void Steer(float moveInput, float turnInput)
    {
        // Logique de direction pour la voiture
        transform.Rotate(0, turnInput * handling * speed * 0.1f * Time.deltaTime, 0);
    }

}
