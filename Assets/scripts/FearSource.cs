using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSource : MonoBehaviour
{
    
    public float Radius;
    public float Strength;
    
    void Awake ()
    {
        FindObjectOfType<Flock>().AddFearSource(this);
    }
    
    void OnDestroy()
    {
        FindObjectOfType<Flock>().RemoveFearSource(this);
    }
}
