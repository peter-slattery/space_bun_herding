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
    }
    
    public void DoShipAction ()
    {
        
    }
    
    public void UpdateShip (float Accel)
    {
        Velocity += (Accel * Heading) - (Velocity * FrictionCo);
        transform.position = transform.position + Velocity * Time.deltaTime;
    }
    
    void Update()
    {
        float Accel = 0;
        if (Input.GetMouseButton(0))
        {
            Vector3 MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HandleShipInput(MouseWorldPosition);
            Accel = Acceleration * Time.deltaTime;
        }
        
        UpdateShip(Accel);
    }
}
