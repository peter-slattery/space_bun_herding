using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlockMember
{
    public Transform MyTransform;
    public float Speed;
    public Vector3 Heading;
}

public class Flock : MonoBehaviour
{
    public FlockMember[] Members;
    public float AwarenessRadius;
    
    public GameObject Prefab_FlockMember;
    
    void Awake ()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject NewMember = GameObject.Instantiate(Prefab_FlockMember);
            NewMember.transform.position = Random.insideUnitCircle * 15;
            Members[i].MyTransform = NewMember.transform;
            Members[i].Speed = 3;
            Members[i].Heading = Random.insideUnitCircle;
        }
    }
    
    void Update ()
    {
        UpdateFlock();
    }
    
    public void UpdateFlockMemberHeading (FlockMember Member, FlockMember[] Members, float AwarenessRadius)
    {
        
    }
    
    public void UpdateFlockMemberPosition (FlockMember Member)
    {
        Member.MyTransform.position += Member.Heading * (Member.Speed * Time.deltaTime);
    }
    
    public void UpdateFlock ()
    {
        for (int i = 0; i < Members.Length; i++)
        {
            UpdateFlockMemberHeading(Members[i], Members, AwarenessRadius);
        }
        
        for (int i = 0; i < Members.Length; i++)
        {
            UpdateFlockMemberPosition(Members[i]);
        }
    }
}
