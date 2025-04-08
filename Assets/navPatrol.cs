using System.Collections;
using System.Collections.Generic;
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
    Vector2 direction = new Vector2(1, 1);
    Vector2 direction1 = new Vector2(-1, 1);
    Vector2 direction2 = new Vector2(0, 1.3f);
    Rigidbody2D thisRigid;
    Vector3 currentPos;
    //Vector2 prevPos;
    Vector3 dir;
    Vector2 rotation;
    





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
        if (agent.remainingDistance < 0.5f)
            GotoNextPoint();

       

        if (!hit)
        {
            hit = Physics2D.Raycast(this.transform.position, direction * rotation); //Change the Vector2.left to whatever direction we want default forward to be.
            hit = Physics2D.Raycast(this.transform.position, direction1 * rotation);
            hit = Physics2D.Raycast(this.transform.position, direction2 * rotation );
            //Debug.Log("checking");
        }

        if (dir.x > 1)
            direction = new Vector2(5, -5);
        else if (dir.x < -1)
            rotation = new Vector2(-5, 5);
        else if (dir.y > 1)
            rotation = new Vector2(5, 5);
        else if (dir.y < -1)
            rotation = new Vector2(-5, -5);
        else
            Debug.Log("nothing");
        

        Debug.Log(dir);

        //Physics2D.Raycast(this.transform.position, new Vector2(1,1), 5f);
    }

    void OnDrawGizmosSelected()
    {
        currentPos = transform.position;
        dir = agent.destination - currentPos;


        Gizmos.color = Color.red;
        Vector2 left = new Vector2(1, 1);
        Vector2 right = new Vector2(-1, 1);
        Vector2 middle = new Vector2(0, 1.3f);

        if (dir.x > 1)
        {
            left = new Vector2(5, 5);
            right = new Vector2(5, -5);
        }
        else if (dir.x < -1)
        {
            left = new Vector2(-5, -5);
            right = new Vector2(-5, 5);
        }
        else if (dir.y > 1)
        {
            left = new Vector2(-5, 5);
            right = new Vector2(5, 5);
        }
        else if (dir.y < -1)
        {
            left = new Vector2(5, -5);
            right = new Vector2(-5, -5);
        }
        else
            Debug.Log("nothing");
        Gizmos.DrawRay(this.transform.position, left);
        Gizmos.DrawRay(this.transform.position, right);
        Gizmos.DrawRay(this.transform.position, middle);

        
    }
}
