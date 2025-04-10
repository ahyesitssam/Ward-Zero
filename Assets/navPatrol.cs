using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class navPatrol : MonoBehaviour
{

    [SerializeField] private Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    RaycastHit2D hit;
    Vector2 left = new Vector2(1, 1);
    Vector2 right = new Vector2(-1, 1);
    Vector2 middle = new Vector2(0, 1.3f);
    Rigidbody2D thisRigid;
    Vector3 currentPos;
    //Vector2 prevPos;
    Vector3 dir;
    Vector2 rotation;
    bool seePlayer = false;
    [SerializeField] private Transform target;
    





    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        


        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        thisRigid = GetComponent<Rigidbody2D>();

        GotoNextPoint();
        currentPos = transform.position;
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;


        // Set the agent to go to the currently selected destination.
        
        agent.destination = points[destPoint].position;
        
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    // Update is called once per frame
    void Update()
    {

        currentPos = transform.position;
        dir = agent.destination - currentPos;
        // Choose the next destination point when the agent gets
        // close to the current one.
        
        if (seePlayer) 
        {
            agent.SetDestination(target.position);
        }
        if (agent.remainingDistance < 0.5f && !seePlayer)
        {
            GotoNextPoint();
        }




        /*Debug.Log(hit.collider);

        if (hit && hit.collider.tag == "Player")
            agent.destination = hit.transform.position;

        if (dir.x > 1)
        {
            left = new Vector2(10, 10);
            right = new Vector2(10, -10);
            middle = new Vector2(10, 0);
        }
        else if (dir.x < -1)
        {
            left = new Vector2(-10, -10);
            right = new Vector2(-10, 10);
            middle = new Vector2(-10, 0);
        }
        else if (dir.y > 1)
        {
            left = new Vector2(-10, 10);
            right = new Vector2(10, 10);
            middle = new Vector2(0, 10f);
        }
        else if (dir.y < -1)
        {
            left = new Vector2(10, -10);
            right = new Vector2(-10, -10);
            middle = new Vector2(0, -10f);
        }
        else
            Debug.Log("nothing");


        if (!hit)
        {
            hit = Physics2D.Raycast(this.transform.position, left); //Change the Vector2.left to whatever direction we want default forward to be.
            hit = Physics2D.Raycast(this.transform.position, right);
            hit = Physics2D.Raycast(this.transform.position, middle);
            //Debug.Log("checking");
        }

        if (hit && hit.collider.tag == "Player")
        {
            
            Debug.Log("Found");
        }*/




        //Debug.Log(dir);

        //Physics2D.Raycast(this.transform.position, new Vector2(1,1), 5f);
    }


    public void foundPlayer()
    {
        seePlayer = true;
        Debug.Log("foundplayer");
    }

    
}
