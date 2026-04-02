using UnityEngine;

public class Bateau : Vehicle
{
    [SerializeField] private float boatBuoyancy;
    protected override void Move()
    {
        if (moveInput > 0)
        {
            speed += acceleration * 0.7f * moveInput * Time.deltaTime;
            // Logique spécifique au bateau
            ApplyBoatBuoyancy();
        }
        
        if (moveInput < 0)
        {
            speed -= brakeForce * 0.6f * Mathf.Abs(moveInput) * Time.deltaTime;
        }

        transform.Rotate(0, turnInput * handling * speed * 0.05f * Time.deltaTime, 0);
    }

    void ApplyBoatBuoyancy()
    {
        // Simuler la flottabilité d'un bateau
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit,
       2.0f))
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
