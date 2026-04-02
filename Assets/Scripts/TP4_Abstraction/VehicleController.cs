using UnityEngine;

public class VehicleController : MonoBehaviour
{
    private Vehicle vehicle;


    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }


    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
        vehicle.UpdateInputs(moveInput, turnInput);
    }
        

        
    /*
    
    
    void ApplyBoatBuoyancy()
    {
        // Simuler la flottabilité d'un bateau
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2f))
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
    */
}