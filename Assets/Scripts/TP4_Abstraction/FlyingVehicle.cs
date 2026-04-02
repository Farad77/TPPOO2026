using UnityEngine;

public abstract class FlyingVehicle : Vehicle
{
    protected override void Drift()
    {
        transform.Rotate(lastTurnInput * handling * (0.5f * baseRotationFactor)* Time.deltaTime, //0.5f for half the baseRotFactor
                            lastMoveInput * handling * (0.3f * baseRotationFactor) * Time.deltaTime, //0.5f for a third of the baseRotFactor
                            -lastTurnInput * handling * baseRotationFactor * Time.deltaTime);
    }
}
