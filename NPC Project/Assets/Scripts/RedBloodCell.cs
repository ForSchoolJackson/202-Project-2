using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBloodCell : Agent
{

    [SerializeField]
    float wanderTime = 1f, wanderRadius = 1f, wanderAngle = 0f;

    [SerializeField]
    float seperationW = 1f, wanderW = 1f, boundsW = 1f, avoidW = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override Vector3 CalculateSteeringForces()
    {
        Vector3 forces = Vector3.zero;

        forces += Wander(wanderTime, wanderRadius, wanderAngle) * wanderW;

        forces += StayInBounds() * boundsW;

        forces += SeparateAgents() * seperationW;

        forces += AvoidObstacles() * avoidW;

        return forces;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (Vector3 obPos in foundObPos)
        {
            Gizmos.DrawLine(transform.position, obPos);
        }

        //
        //  Draw safe space box
        //
        Vector3 futurePos = CalculateFuturePos(wanderTime);

        float length = Vector3.Distance(transform.position, futurePos) + physicsObject.radius;

        Vector3 boxSize = new Vector3(physicsObject.radius * 2f,
            length
            , physicsObject.radius * 2f);

        Vector3 boxCenter = Vector3.zero;
        boxCenter.y += length / 2f;

        Gizmos.color = Color.green;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(boxCenter, boxSize);
        Gizmos.matrix = Matrix4x4.identity;

        //
        //  Draw lines to found obstacles
        //
        Gizmos.color = Color.yellow;

        foreach (Vector3 pos in foundObPos)
        {
            Gizmos.DrawLine(transform.position, pos);
        }

        Gizmos.color = Color.white;
             Gizmos.DrawWireSphere(physicsObject.position, length);
    }
}
