using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    [SerializeField]
    public Vector3 position, velocity;

    private Vector3 direction, acceleration;

    public Vector3 Velocity { get { return velocity; } }

    [SerializeField]
    public float backgroundRadius = 4.7f;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }

    }

    [SerializeField]
    public float mass, maxSpeed;
 
    private float frictionCoeff = 0f;

    [SerializeField]
    public float radius;

    //things that might go away
    private bool onFriction;
    private bool onGravity;

    public SpriteRenderer spriteRenderer;

    Vector2 screenSize = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();

        radius = spriteRenderer.bounds.extents.y;

        //camera size
        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = screenSize.y * Camera.main.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        if (onFriction)
        {
            ApplyFriction(frictionCoeff);
        }

        if (onGravity)
        {
            ApplyGravity(Vector3.down * 9.81f);
        }

        //calc velocity
        velocity += acceleration * Time.deltaTime;

        //limit how much velocity an object can observe each frame
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        Bounce();

        position += velocity * Time.deltaTime;

        //grab current direction
        direction = velocity.normalized;

        //rotate 
        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

        transform.position = position;

        //zero out acceleration
        acceleration = Vector3.zero;

    }

    //total force
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    //force of firction
    public void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();

        friction = friction * coeff;

        ApplyForce(friction);


    }

    //force of gravity
    public void ApplyGravity(Vector3 force)
    {
        acceleration += force;

    }

    //bounce monsters
    public void Bounce()
    {
        // float camWidth = sceneCamera.orthographicSize * sceneCamera.aspect;
        // float camHeight = sceneCamera.orthographicSize;

        float distance = Vector3.Distance(new Vector3(position.x, position.y, 0f), Vector3.zero);

        //if off screen in x
       // if (position.x >= screenSize.x)
       // {
       //     position.x = screenSize.x;
       //     velocity.x *= -1f;
       //     //direction.x += -1f
       // }
       // if (position.x <= -screenSize.x)
       // {
       //     position.x = -screenSize.x;
       //     velocity.x *= -1f;
       //     //direction.x += -1f
       // }
       //
       // //if off screen in y
       // if (position.y >= screenSize.y)
       // {
       //     position.y = screenSize.y;
       //     velocity.y *= -1f;
       //     //direction.x += -1f
       // }
       // if (position.y <= -screenSize.y)
       // {
       //     position.y = -screenSize.y;
       //     velocity.y *= -1f;
       //     //direction.x += -1f
       // }

        //out of circle
        if(distance >= backgroundRadius)
        {
            Vector3 towardCenter = (Vector3.zero - new Vector3(position.x, position.y)).normalized;

            // Calculate the new position by moving towards the center of the circle
            position += towardCenter * (distance - backgroundRadius);

            // Reflect the velocity based on the direction to the center
            velocity = Vector2.Reflect(velocity, towardCenter);
        }
    }
}
