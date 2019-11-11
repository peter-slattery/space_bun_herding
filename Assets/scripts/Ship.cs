using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 Heading; // Direction of Thrust
    public float Acceleration; // Amount of Thrust
    public float FrictionCo;
    public Vector3 Velocity;
    public GameObject Prefab_Bomb;
    
    public AudioSource ShipHum;
    public float MaxSpeed;
    public float VolumeRange;
    public float VolumeMin;
    
    public void HandleShipInput (Vector3 TargetPosition)
    {
        Vector3 PositionToTarget = TargetPosition - transform.position;
        PositionToTarget.z = 0;
        Heading = (PositionToTarget.normalized);
    }
    
    private void OnMouseDown()
    {
        DropBomb();
    }
    
    public void DropBomb ()
    {
        GameObject bomb = GameObject.Instantiate(Prefab_Bomb);
        bomb.transform.position = transform.position;
    }
    
    public void UpdateShip (float Accel)
    {
        Velocity += (Accel * Heading) - (Velocity * FrictionCo);
        transform.position = transform.position + Velocity * Time.deltaTime;
        
        float RotationZ = -1 * Mathf.Atan2(Heading.x, Heading.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, RotationZ);
        
        float PercentMaxSpeed = Velocity.magnitude / MaxSpeed;
        ShipHum.volume = (PercentMaxSpeed * VolumeRange) + VolumeMin;
    }

    void Update()
    {
        float Accel = 0;
        Vector3 MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HandleShipInput(MouseWorldPosition);
        Accel = Acceleration * Time.deltaTime;
        UpdateShip(Accel);

        if (Input.GetMouseButtonDown(0) ||
            Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }
    }
}
