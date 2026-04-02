using UnityEngine;

public class Boat : Vehicle
{
    private float boatBuoyancy;

    public override void Accelerate(float moveInput, float turnInput)
    {
        speed += acceleration * 0.7f * moveInput * Time.deltaTime;
        // Logique spécifique au bateau
        ApplyBoatBuoyancy();
    }

    public override void Brake()
    {
    }

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
}
