using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class SpawnedEnemy : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    RaycastHit2D hit;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        target = player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; //May need to be changed later
        agent.updateUpAxis = false;
        agent.speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
