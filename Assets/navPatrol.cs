using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

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
    Vector2 currentPos;
    Vector2 prevPos;


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
        StartCoroutine(checkPrevPos());

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.5f)
            GotoNextPoint();

        if ((prevPos.x - currentPos.x) > 0) 
        {
            Debug.Log("direction?");
        }

        if (!hit)
        {
            hit = Physics2D.Raycast(this.transform.position, direction); //Change the Vector2.left to whatever direction we want default forward to be.
            hit = Physics2D.Raycast(this.transform.position, direction1);
            hit = Physics2D.Raycast(this.transform.position, direction2);
            //Debug.Log("checking");
        }


        
        //Physics2D.Raycast(this.transform.position, new Vector2(1,1), 5f);
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        
        
        Gizmos.color = Color.red;
        Vector2 direction = new Vector2(1, 1);
        Vector2 direction1 = new Vector2(-1, 1);
        Vector2 direction2 = new Vector2(0, 1.3f);
        Gizmos.DrawRay(this.transform.position, direction);
        Gizmos.DrawRay(this.transform.position, direction1);
        Gizmos.DrawRay(this.transform.position, direction2);


    }
    IEnumerator checkPrevPos()
    {
        prevPos = currentPos;
        Debug.Log(prevPos.x - currentPos.x);
        yield return new WaitForSeconds(0.5f);
    }
}
