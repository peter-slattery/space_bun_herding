using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 Heading; // Direction of Thrust
    public float Acceleration; // Amount of Thrust
    public float FrictionCo;
    public Vector3 Velocity;
    
    public void HandleShipInput (Vector3 TargetPosition)
    {
        Vector3 PositionToTarget = TargetPosition - transform.position;
        PositionToTarget.z = 0;
        Heading = (PositionToTarget.normalized);
        Velocity += Acceleration * Heading * Time.deltaTime;
    }
    
    public void DoShipAction ()
    {
        
    }
    
    public void UpdateShip ()
    {
        Velocity += -Velocity * FrictionCo * Time.deltaTime;
        transform.position = transform.position + Velocity * Time.deltaTime;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HandleShipInput(MouseWorldPosition);
        }
        
        UpdateShip();
    }
}