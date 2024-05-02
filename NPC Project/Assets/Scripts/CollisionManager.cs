using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{ 

    [SerializeField]
    Camera sceneCamera;

    // Update is called once per frame
    void Update()
    {

        //when collision detected change thing
    }

    //check using radii
    public bool CircleCollisionCheck(PhysicsObject spriteA, PhysicsObject spriteB)
    {

        // Debug.Log("Checking collision");

        float distanceSq = (spriteB.transform.position - spriteA.transform.position).sqrMagnitude;

        if (distanceSq < Mathf.Pow((spriteA.radius + spriteB.radius), 2))
        {
            Debug.Log("HIT");
            return true;
        }
        else
        {
            //  Debug.Log("None");
            return false;
        }

    }

    Vector3 RandomPosition()
    {

        float camWidth = sceneCamera.orthographicSize * sceneCamera.aspect;
        float camHeight = sceneCamera.orthographicSize;

        float randomX = Random.Range(-camWidth, camWidth);
        float randomY = Random.Range(-camHeight, camHeight);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

        //Debug.Log(randomPosition);

        return randomPosition;




    }
}
