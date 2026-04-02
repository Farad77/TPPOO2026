using NUnit.Framework;
using System.Collections.Generic;
using TP3_Polymorphisme_Nicolas;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField]
    private List<Vehicule> m_vehiculesList;
    [SerializeField]
    private Vehicule currentVehicule;

    [SerializeField]
    private int actualVehiculIndex = 0;

    [SerializeField]
    private IsometricCamera isometricCamera;

    [SerializeField]
    private Transform spawnPoint;

    void Start()
    {
        if (m_vehiculesList.Count > 0)
        {
            SwitchVehicule(m_vehiculesList[actualVehiculIndex]);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            NextVehicule();
        }

        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");


        if (currentVehicule != null)
        {

            currentVehicule.Move(moveInput, turnInput);
        }
    }

    public void NextVehicule()
    {
        if (m_vehiculesList.Count == 0)
            return;
        Debug.Log("Switching weapon...");
        actualVehiculIndex = (actualVehiculIndex + 1) % m_vehiculesList.Count;
        SwitchVehicule(m_vehiculesList[actualVehiculIndex]);
    }


    public void SwitchVehicule(Vehicule vehicule)
    {
        if (currentVehicule != null)
        {
            Destroy(currentVehicule.gameObject);
        }


        m_vehiculesList.ForEach(v =>
        {
            if (v.name == vehicule.name)
            {
                Vehicule tempVehicule = Instantiate(v, spawnPoint.position, Quaternion.identity);
                isometricCamera.SetNewTarget(tempVehicule.transform);

                currentVehicule = tempVehicule;
            }
        });

    }





}