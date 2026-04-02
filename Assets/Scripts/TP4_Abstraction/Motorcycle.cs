using UnityEngine;

public class Motorcycle : GroundVehicle
{
    [Header("Motorcycle Attributes")]
    [SerializeField] private float motorcycleLeanAngle;
    protected override void MovementComportment()
    {
        float targetLean = -lastTurnInput * motorcycleLeanAngle;
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.z = Mathf.LerpAngle(currentRotation.z, targetLean, Time.deltaTime * 2.0f);
        transform.localEulerAngles = currentRotation;
    }
}
