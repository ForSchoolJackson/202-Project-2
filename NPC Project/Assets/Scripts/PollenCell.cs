using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenCell : Agent
{

    [SerializeField]
    float wanderTime = 1f, wanderRadius = 1f, wanderAngle = 0f;

    [SerializeField]
    float seperationW = 1f, wanderW= 1f, fleeW = 1f, boundsW = 1f, avoidW = 1f;

    [SerializeField]
    float getsClose = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override Vector3 CalculateSteeringForces()
    {
        Vector3 forces = Vector3.zero;

        if (agentManager.pollenArray.Count > 0)
        {
            //find the closes agent to follow
            Agent whiteClose = agentManager.whiteArray[0];
            float distClose = Vector3.Distance(transform.position, whiteClose.transform.position);
            foreach (Agent white in agentManager.whiteArray)
            {
                float distance = Vector3.Distance(transform.position, white.transform.position);
                if (distance < distClose)
                {
                    distClose = distance;
                    whiteClose = white;
                }
            }
      
            //check if close 
            if(distClose < getsClose)
            {
                //flee
                forces += Flee(whiteClose.transform.position) * fleeW;
            }
            else
            {
                //wander
                forces += Wander(wanderTime, wanderRadius, wanderAngle) * wanderW;
                
            }
      
            
        }

        

        forces += StayInBounds() * boundsW;

        forces += SeparateAgents() * seperationW;

        forces += AvoidObstacles() * avoidW;

        return forces;
    }
}
