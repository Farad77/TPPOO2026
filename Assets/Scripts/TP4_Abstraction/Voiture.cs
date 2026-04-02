using UnityEngine;

public class Voiture : Vehicle
{

    public float carTraction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    void Update()
    {
        if (moveInput > 0)
        {
            speed += acceleration * moveInput * Time.deltaTime;
            // Logique spécifique à la voiture
            ApplyCarTraction();
        }
    }
}
