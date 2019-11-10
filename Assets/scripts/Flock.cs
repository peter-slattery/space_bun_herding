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
    public float BaseSpeed;
    public int Count;
    public float InitialSpread;
    
    public GameObject Prefab_FlockMember;
    
    public float AlignmentWeight;
    public float CohesionWeight;
    public float SeparationWeight;
    public float BoundsWeight;
    
    void Awake ()
    {
        Members = new FlockMember[Count];
        
        for (int i = 0; i < Count; i++)
        {
            GameObject NewMember = GameObject.Instantiate(Prefab_FlockMember);
            NewMember.transform.position = Random.insideUnitCircle * InitialSpread;
            
            Members[i] = new FlockMember();
            Members[i].MyTransform = NewMember.transform;
            Members[i].Speed = BaseSpeed;
            Members[i].Heading = Random.insideUnitCircle;
        }
    }
    
    void Update ()
    {
        UpdateFlock();
    }
    
    public void UpdateFlockMemberHeading (FlockMember Member, FlockMember[] Members, float AwarenessRadius)
    {
        int AwareOfOthersCount = 0;
        Vector3 AveragePosition = Vector3.zero;
        Vector3 AverageHeading = Vector3.zero;
        Vector3 SeparationDirection = Vector3.zero;
        
        for (int m = 0; m < Members.Length; m++)
        {
            FlockMember Other = Members[m];
            if (Other == Member) { continue; }
            
            float DistanceToOther = Vector3.Distance(Other.MyTransform.position, 
                                                     Member.MyTransform.position);
            if (DistanceToOther < AwarenessRadius)
            {
                AwareOfOthersCount++;
                AveragePosition += Other.MyTransform.position;
                AverageHeading += Other.Heading;
                SeparationDirection += (Member.MyTransform.position - Other.MyTransform.position);
            }
        }
        
        Vector3 AwayFromWall = Vector3.zero;
        
        if (Member.MyTransform.position.x < -2)
        {
            AwayFromWall += new Vector3(1, 0, 0);
        }
        else if (Member.MyTransform.position.x > 2)
        {
            AwayFromWall += new Vector3(-1, 0, 0);
        }
        if (Member.MyTransform.position.y < -5)
        {
            AwayFromWall += new Vector3(0, 1, 0);
        }
        else if (Member.MyTransform.position.y > 5)
        {
            AwayFromWall += new Vector3(0, -1, 0);
        }
        
        if (AwareOfOthersCount > 0)
        {
            AveragePosition = AveragePosition / AwareOfOthersCount;
            Vector3 TowardsAveragePosition = AveragePosition - Member.MyTransform.position;
            
            AverageHeading = AverageHeading / AwareOfOthersCount;
            
            SeparationDirection = SeparationDirection.normalized;
            
            Member.Heading += ((TowardsAveragePosition * CohesionWeight) + 
                               (AverageHeading * AlignmentWeight) + 
                               (SeparationDirection * SeparationWeight) +
                               (AwayFromWall * BoundsWeight)) * .5f;
            Member.Heading = Member.Heading.normalized;
        }
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
