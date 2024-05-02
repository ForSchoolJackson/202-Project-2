using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCell : Agent
{

    [SerializeField]
    float wanderTime = 1f, wanderRadius = 1f, wanderAngle = 0f;

    [SerializeField]
    float seperationW = 1f, seekW = 1f, wanderW = 1f, boundsW = 1f, avoidW = 2f;

    [SerializeField]
    float caughtNum = 2f;

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
           Agent polClose = agentManager.pollenArray[0];
            float distClose = Vector3.Distance(transform.position, polClose.transform.position);
            foreach (Agent pollen in agentManager.pollenArray)
            {
                float distance = Vector3.Distance(transform.position, pollen.transform.position);
                if (distance < distClose)
                {
                    distClose = distance;
                    polClose = pollen;
                }
            }

            //if it catches it
            if (distClose < caughtNum)
            {
                //remove from the arrays
                //agentManager.pollenArray.Remove(polClose);
                // agentManager.agents.Remove(polClose);

                if (agentManager.pollenArray.Contains(polClose))
                {

                    agentManager.pollenArray.RemoveAll(agent => agent == polClose);
                    agentManager.agents.RemoveAll(agent => agent == polClose);

                    //delete
                    Destroy(polClose.gameObject);

                }
            }
            
            forces += Seek(polClose.transform.position) * seekW;
        }
        else
        {
            forces += Wander(wanderTime, wanderRadius, wanderAngle) * wanderW;
        }
        

        forces += StayInBounds() * boundsW;

        forces += SeparateAgents() * seperationW;

        forces += AvoidObstacles() * avoidW;

        return forces;
    }

   // private void OnDrawGizmos()
  //  {
   //     Gizmos.color = Color.white;
  //      Gizmos.DrawRay(transform.position, Seek(target));
  //  }
}
