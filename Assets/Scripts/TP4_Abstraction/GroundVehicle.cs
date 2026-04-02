using UnityEngine;

public abstract class GroundVehicle : Vehicle
{
    protected override void Drift()
    {
        transform.Rotate(0, lastTurnInput * handling * speed * baseRotationFactor * Time.deltaTime, 0);
    }
}
