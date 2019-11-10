using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bun : MonoBehaviour
{
    public Collider2D bunCollider;
    public bool inBounds;
    
    void Awake ()
    {
        FindObjectOfType<Shape>().AddBun(this);
    }
}
