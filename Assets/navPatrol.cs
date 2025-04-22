using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
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

    NavMeshPath path;
    bool checkingLeft;




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
        path = new NavMeshPath();
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

        if (agent.CalculatePath(target.position, path) && path.status == NavMeshPathStatus.PathComplete && seePlayer)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            //agent.destination = agent.transform.position;
            seePlayer = false;
            /*if (!checkingLeft) 
            {
                StartCoroutine(checkLeft());
                checkingLeft = true;
            }*/
        }
            if (agent.remainingDistance < 0.5f && !seePlayer)
        {
            GotoNextPoint();
        }

    }


    public void foundPlayer()
    {
        seePlayer = true;
        Debug.Log("foundplayer");
    }

    IEnumerator checkLeft() 
    {
        yield return new WaitForSeconds(3);
        if(!(agent.CalculatePath(target.position, path) && path.status == NavMeshPathStatus.PathComplete) && seePlayer)
        {
            Destroy(this.gameObject);
        }
    }
    
}
