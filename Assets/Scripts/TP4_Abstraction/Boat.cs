using UnityEngine;

public class Boat : Vehicule
{
    [SerializeField]
    public float boatBuoyancy;


    void ApplyBoatBuoyancy()
    {
        // Simuler la flottabilité d'un bateau
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2.0f))
        {
            if (hit.collider.CompareTag("Water"))
            {
                float desiredHeight = hit.point.y + boatBuoyancy;
                Vector3 pos = transform.position;
                pos.y = Mathf.Lerp(pos.y, desiredHeight, Time.deltaTime * 2.0f);
                transform.position = pos;
            }
        }
    }

    public override void Accelerate(float moveInput, float turnInput)
    {
        Speed += Acceleration * 0.7f * moveInput * Time.deltaTime;
        // Logique spécifique au bateau
        ApplyBoatBuoyancy();
    }

    public override void Brake(float moveInput, float turnInput)
    {
        Speed -= BrakeForce * 0.6f * Mathf.Abs(moveInput) * Time.deltaTime;
    }

    public override void Steer(float moveInput, float turnInput)
    {
        // Logique de direction pour le bateau
        transform.Rotate(0, turnInput * Handling * Speed * 0.05f * Time.deltaTime, 0);
    }

}
