using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlockMember
{
    public Transform MyTransform;
    public Vector3 Velocity;
    public Vector3 Heading;
}

public class Flock : MonoBehaviour
{
    public FlockMember[] Members;
    public List<FearSource> FearSources;
    
    public float AwarenessRadius;
    public float BaseAcceleration;
    public float BaseFrictionCo;
    public int Count;
    public float InitialSpread;
    
    public GameObject Prefab_FlockMember;
    
    public float AlignmentWeight;
    public float CohesionWeight;
    public float SeparationWeight;
    public float FearWeight;
    public float BoundsWeight;
    
    public void AddFearSource (FearSource NewFearSource)
    {
        FearSources.Add(NewFearSource);
    }
    
    public void RemoveFearSource (FearSource ToRemove)
    {
        FearSources.Remove(ToRemove);
    }
    
    void Awake ()
    {
        Members = new FlockMember[Count];
        
        for (int i = 0; i < Count; i++)
        {
            GameObject NewMember = GameObject.Instantiate(Prefab_FlockMember);
            NewMember.transform.position = Random.insideUnitCircle * InitialSpread;
            
            Members[i] = new FlockMember();
            Members[i].MyTransform = NewMember.transform;
            Members[i].Velocity = Vector3.zero;
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
        Vector3 FearDirection = Vector3.zero;
        
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
        
        for (int f = 0; f < FearSources.Count; f++)
        {
            FearSource Source = FearSources[f];
            float DistanceToSource = Vector3.Distance(Member.MyTransform.position,
                                                      Source.transform.position);
            if (DistanceToSource < Source.Radius)
            {
                FearDirection += (Member.MyTransform.position - Source.transform.position).normalized * Source.Strength;
            }
        }
        
        float XDistanceFromCenter = Mathf.Abs(Member.MyTransform.position.x);
        float YDistanceFromCenter = Mathf.Abs(Member.MyTransform.position.y);
        float XDirToCenter = -1 * Mathf.Sign(Member.MyTransform.position.x);
        float YDirToCenter = -1 * Mathf.Sign(Member.MyTransform.position.y);
        float ReturnToXCenterWeight = XDistanceFromCenter / 2;
        float ReturnToYCenterWeight = YDistanceFromCenter / 5;
        
        Vector3 AwayFromWall = new Vector3(ReturnToXCenterWeight * XDirToCenter,
                                           ReturnToYCenterWeight * YDirToCenter,
                                           0);
        
        Vector3 TowardsAveragePosition = Vector3.zero;
        if (AwareOfOthersCount > 0)
        {
            AveragePosition = AveragePosition / AwareOfOthersCount;
            TowardsAveragePosition = AveragePosition - Member.MyTransform.position;
            
            AverageHeading = AverageHeading / AwareOfOthersCount;
        }
        
        SeparationDirection = SeparationDirection.normalized;
        
        if (FearSources.Count > 0)
        {
            FearDirection = FearDirection / FearSources.Count;
        }
        
        Member.Heading += ((TowardsAveragePosition * CohesionWeight) + 
                           (AverageHeading * AlignmentWeight) + 
                           (SeparationDirection * SeparationWeight) +
                           (FearDirection * FearWeight) + 
                           (AwayFromWall * BoundsWeight)) * .5f;
        Member.Heading = Member.Heading.normalized;
        Member.Heading.z = 0;
    }
    
    public void UpdateFlockMemberPosition (FlockMember Member)
    {
        float Accel = BaseAcceleration * Time.deltaTime;
        Member.Velocity += (Accel * Member.Heading) - (Member.Velocity * BaseFrictionCo);
        Member.MyTransform.position += Member.Velocity * Time.deltaTime;
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
