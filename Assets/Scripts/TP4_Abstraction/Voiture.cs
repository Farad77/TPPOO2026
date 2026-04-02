using UnityEngine;

public class Voiture : Vehicle
{

    public float carTraction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        if (moveInput > 0)
        {
            speed += acceleration * moveInput * Time.deltaTime;
            // Logique spécifique à la voiture
            ApplyCarTraction();
        }
        else if (moveInput < 0)
        {
            speed -= brakeForce * Mathf.Abs(moveInput) * Time.deltaTime;
        }
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        transform.Rotate(0, turnInput * handling * speed * 0.1f * Time.deltaTime, 0);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
