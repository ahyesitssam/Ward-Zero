using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using System.IO;

public class SpawnedEnemy : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    RaycastHit2D hit;
    GameObject player;
    NavMeshPath path;
    bool checkingLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        target = player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; //May need to be changed later
        agent.updateUpAxis = false;
        agent.speed = 5.0f;
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.CalculatePath(target.position, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            
            agent.destination = agent.transform.position;
            if (!checkingLeft) 
            {
                StartCoroutine(checkLeft());
                checkingLeft = true;
            }
        }
    }
    IEnumerator checkLeft()
    {
        yield return new WaitForSeconds(3);
        if (!(agent.CalculatePath(target.position, path) && path.status == NavMeshPathStatus.PathComplete))
        {
            Destroy(this.gameObject);
        }
    }
}
