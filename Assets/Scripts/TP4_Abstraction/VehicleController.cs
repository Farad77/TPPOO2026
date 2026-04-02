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
}