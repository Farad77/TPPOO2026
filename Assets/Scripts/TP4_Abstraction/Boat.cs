using UnityEngine;

public class Boat : GroundVehicle
{
    [Header("Boat Attributes")]
    [SerializeField] private float boatBuoyancy;
    protected override void MovementComportment()
    {
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
}
