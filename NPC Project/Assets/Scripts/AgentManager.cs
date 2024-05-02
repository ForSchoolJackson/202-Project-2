using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentManager : MonoBehaviour
{

    [SerializeField]
    public Background background;

    [SerializeField]
    Agent redBl, whiteBl, pollen;

    [SerializeField]
    int redCount = 100, whiteCount = 5, pollenCount = 5;

    public List<Agent> agents;

    //list for specifics
    public List<Agent> pollenArray;
    public List<Agent> whiteArray;

    public List <Obstacle> obstacles;

    Vector2 screenSize = Vector2.zero;

    //for the spawning
    Vector3 mousePosition;
    bool isClickL = false;
    bool isClickR = false;
    float timer = 0f;

    public Vector2 ScreenSize
    {
        get { return screenSize; }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = screenSize.y * Camera.main.aspect;

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = (Vector3)Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        SpawnPollen();
    }

    void Spawn()
    {
        //spawn red blood cells
        for (int i = 0; i < redCount; ++i)
        {
            Agent newAgent = Instantiate(redBl, PickRandomPosition(), Quaternion.identity);

            newAgent.agentManager = this;
            agents.Add(newAgent);
        }

        //spawn white blood cells
        for (int i = 0; i < whiteCount; ++i)
        {
            Agent newAgent = Instantiate(whiteBl, PickRandomPosition(), Quaternion.identity);

            newAgent.agentManager = this;
            agents.Add(newAgent);
            whiteArray.Add(newAgent);
        }

        //spawn white pollen cells
        for (int i = 0; i < pollenCount; ++i)
        {
            Agent newAgent = Instantiate(pollen, PickRandomPosition(), Quaternion.identity);

            newAgent.agentManager = this;
            agents.Add(newAgent);
            pollenArray.Add(newAgent);
        }
    }

    //random pos in circle
    Vector2 PickRandomPosition()
    {
        Vector2 randPos = Vector2.zero;

        //randPos.x = Random.Range(-screenSize.x, screenSize.x);
        //randPos.y = Random.Range(-screenSize.y, screenSize.y);

        Debug.Log(background.radiusInt);

        if (background != null)
        {
            randPos = (Random.insideUnitCircle * background.radiusInt) ;
        }

        return randPos;
    }

    //add a new poleen cell
    void SpawnPollen()
    {
        if (isClickL)
        {
            //only spawn if the timer is less than zero
            if (timer <= 0f)
            {
                mousePosition.z = 0f;

                Agent newAgent = Instantiate(redBl, mousePosition, Quaternion.identity);

                newAgent.agentManager = this;
                agents.Add(newAgent);

                isClickL = false;
                timer = 0.05f;
            }
            else
            {
                //have timer go down
                timer -= Time.deltaTime;
            }
        }

        if (isClickR)
        {
            //only spawn if the timer is less than zero
            if (timer <= 0f)
            {
                mousePosition.z = 0f;

                Agent newAgent = Instantiate(pollen, mousePosition, Quaternion.identity);

                newAgent.agentManager = this;
                agents.Add(newAgent);
                pollenArray.Add(newAgent);

                isClickR = false;
                timer = 0.05f;
            }
            else
            {
                //have timer go down
                timer -= Time.deltaTime;
            }
        }
    }

    //mouse click function
    public void MouseClickLeft(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            Debug.Log("mouse clicked");
            isClickL = true;
        }
        else if (context.canceled)
        {
            isClickL = false;
        }
        

    }

    //mouse click function
    public void MouseClickRight(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            Debug.Log("mouse clicked");
            isClickR = true;
        }
        else if (context.canceled)
        {
            isClickR = false;
        }


    }
}
