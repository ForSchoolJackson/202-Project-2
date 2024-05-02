using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{

    [SerializeField]
    public PhysicsObject physicsObject;

    protected List<Vector3> foundObPos = new List<Vector3>();

    Vector2 screenSize = Vector2.zero;

    protected Vector3 totalForces = Vector3.zero;

    [SerializeField]
    float maxForce = 5f;

    public AgentManager agentManager;

    Vector3 futurePos;

    // Update is called once per frame
    void Update()
    {
        totalForces += CalculateSteeringForces();

        totalForces = Vector3.ClampMagnitude(totalForces, maxForce);

        physicsObject.ApplyForce(totalForces);

        totalForces = Vector3.zero;
    }

    protected abstract Vector3 CalculateSteeringForces();

    //SEEKING BEHAVIOR
    public Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desiredVelocity = targetPos - transform.position;

        desiredVelocity = desiredVelocity.normalized * physicsObject.maxSpeed;

        Vector3 seekingForce = desiredVelocity - physicsObject.Velocity;

        return seekingForce;
    }
    public Vector3 Seek(Agent target)
    {
        return Seek(target.transform.position);
    }

    //FLEEING BEHAVIOR
    public Vector3 Flee(Vector3 targetPos)
    {
        Vector3 desiredVelocity = transform.position - targetPos;

        desiredVelocity = desiredVelocity.normalized * physicsObject.maxSpeed;

        Vector3 fleeingForce = desiredVelocity - physicsObject.Velocity;

        return fleeingForce;
    }
    public Vector3 Flee(Agent target)
    {
        return Flee(target.transform.position);
    }
    public Vector3 Evade(Agent target)
    {
        return Flee(target.CalculateFuturePos(5f));
    }

    //WANDERING BEHAVIOR
    public Vector3 Wander(float futureTime, float circleRadius, float angle)
    {
         futurePos = CalculateFuturePos(futureTime);

        //float randAngle = Random.Range(0f, Mathf.PI * 2f);
        angle += Random.Range(-2f, 2f);

        Vector3 wanderPoint = futurePos;
        wanderPoint.x += Mathf.Cos(angle) * circleRadius;
        wanderPoint.y += Mathf.Sin(angle) * circleRadius;

        return Seek(wanderPoint);
    }

    //calcualte the future
    public Vector3 CalculateFuturePos(float futureTime)
    {
        return transform.position + (physicsObject.Velocity * futureTime);
    }

    //STAYING IN BOUNDS BEHAVIOR
    public Vector3 StayInBounds()
    {
        Vector3 steeringForce = Vector3.zero;

        if (CheckBounds(transform.position))
        {
            //Do stuff
            steeringForce = Seek(Vector3.zero);
        }

        return steeringForce;
    }

    //check for off screen
    protected bool CheckBounds(Vector3 point)
    {
        
        float distance = Vector2.Distance(new Vector2(point.x, point.y), agentManager.background.transform.position);

        //if off petree dish
        if (distance >= agentManager.background.radiusInt)
        {
            return true;
           
         }
         else
         {
             return false;
         }

    }

    //seperation
    public Vector3 SeparateAgents()
    {
        Vector3 seperationForce = Vector3.zero;

        //see all agents that are spawned in
        foreach(Agent agent in agentManager.agents)
        {
            float distance = Vector3.Distance(transform.position, agent.transform.position);
            if (Mathf.Epsilon < distance)
            {
                seperationForce += Flee(agent) * (1f / distance);
            }
        }
        return seperationForce;

    }

    //obstacle voidance
    public Vector3 AvoidObstacles()
    {
        Vector3 steeringForce = Vector3.zero;
        foundObPos.Clear();

        Vector3 vToO = Vector3.zero;
        Vector3 vectorR;
        float forwardDot, rightDot, leftDot;
 
        float length = Vector3.Distance(transform.position, futurePos) + physicsObject.radius;


        foreach (Obstacle obs in agentManager.obstacles)
        {
            vToO = obs.transform.position - transform.position;

            forwardDot = Vector3.Dot(physicsObject.Direction, vToO);

            vectorR = Vector3.Cross(physicsObject.Direction, Vector3.back).normalized;

            rightDot = Vector3.Dot(vectorR, vToO);
            leftDot = Vector3.Dot(-vectorR, vToO);


            if (forwardDot > 0f- obs.radius)
            {
                if (forwardDot < length + obs.radius)
                {
                    //if oject is in box
                    if(Mathf.Abs(rightDot) < physicsObject.radius + obs.radius)
                    {
                        //found something
                        foundObPos.Add(obs.transform.position);

                        //steering force right
                        steeringForce = transform.right * physicsObject.maxSpeed;

                        if (Mathf.Abs(leftDot) <= physicsObject.radius + obs.radius)
                        {
                            //steering force to the left
                            steeringForce = -steeringForce;
                        }
                    }
                    
                }
                   
                
            }
            
        }

        return steeringForce;
    }

}
