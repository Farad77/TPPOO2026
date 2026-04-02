using UnityEngine;

public class Plane : FlyingVehicle
{
    [Header("Plane Attributes")]
    [SerializeField] private float airplaneLift;
    protected override void MovementComportment()
    {
        if (speed > maxSpeed * 0.3f)
        {
            float liftForce = airplaneLift * (speed / maxSpeed);
            transform.Translate(Vector3.up * liftForce * Time.deltaTime, Space.World);
        }
    }
}
