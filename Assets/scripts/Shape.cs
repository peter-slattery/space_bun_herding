using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shape : MonoBehaviour
{

    public Vector2[] shapeVertices; //bounding points of shape
    public PolygonCollider2D shapeCollider;


    public Bun[] herd; //herd to be contained within bounds

    void Awake()
    {

        //shapeCollider.points = shapeVertices;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkHerdInBounds(herd) == true)
        {
            Debug.Log("All buns in bounds");
            Camera.main.backgroundColor = Color.green;
        }
        else
        {
            Debug.Log("Buns NOT in bounds");
            Camera.main.backgroundColor = Color.red;
        }
    }

    bool checkHerdInBounds(Bun[] herdToCheck)
    {
        foreach (Bun bunToCheck in herdToCheck)
        {
            if (checkBunInBounds(bunToCheck) == false) {
                return false;
            }
        }

        return true;
    }

    bool checkBunInBounds(Bun bunToCheck)
    {
        Vector2 bunPoint;

        bunPoint = bunToCheck.transform.position;

        if(shapeCollider.bounds.Contains(bunPoint))
        {
            Debug.Log("Bounds contain the point : " + bunPoint);
            return true;
        } else
        {
            Debug.Log("Bounds DO NOT contain the point : " + bunPoint);
            return false;
        }
    }

}


/*
//Attach this script to a GameObject with a Collider component
//Create an empty GameObject (Create>Create Empty) and attach it in the New Transform field in the Inspector of the first GameObject
//This script tells if a point  you specify (the position of the empty GameObject) is within the first GameObject’s Collider


public class Example : MonoBehaviour
{
    //Make sure to assign this in the Inspector window
    public Transform m_NewTransform;
    Collider m_Collider;
    Vector3 m_Point;

    void Start()
    {
        //Fetch the Collider from the GameObject this script is attached to
        m_Collider = GetComponent<Collider>();
        //Assign the point to be that of the Transform you assign in the Inspector window
        m_Point = m_NewTransform.position;
    }

    void Update()
    {
        //If the first GameObject's Bounds contains the Transform's position, output a message in the console
        if (m_Collider.bounds.Contains(m_Point))
        {
            Debug.Log("Bounds contain the point : " + m_Point);
        }
    }
}
*/