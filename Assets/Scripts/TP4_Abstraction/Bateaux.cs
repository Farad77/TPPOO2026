using UnityEngine;

public class Bateaux : Vehicle
{

    public float boatBuoyancy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        if (moveInput > 0)
        {
            speed += acceleration * 0.7f * moveInput * Time.deltaTime;
            // Logique spécifique au bateau
            ApplyBoatBuoyancy();
        }

        else if (moveInput < 0)
        {
            speed -= brakeForce * 0.6f * Mathf.Abs(moveInput) * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        transform.Rotate(0, turnInput * handling * speed * 0.05f * Time.deltaTime, 0);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

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
}
