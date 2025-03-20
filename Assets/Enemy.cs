using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; //May need to be changed later
        agent.updateUpAxis = false;
        agent.speed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), Vector2.left, 5.0f); //Change the Vector2.left to whatever direction we want default forward to be.

        if (hit && hit.collider.tag == "Player") 
        {
            agent.speed = 5.0f;       
        }
    }
}
