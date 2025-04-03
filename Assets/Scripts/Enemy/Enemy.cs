using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] Transform target;
    NavMeshAgent agent;
    RaycastHit2D hit;
    

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
        if (!hit) 
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), transform.right, 5.0f); //Change the Vector2.left to whatever direction we want default forward to be.
            //Debug.Log("checking");
        }


        if (hit && hit.collider.tag == "Player") 
        {
            agent.speed = 5.0f;
            Debug.Log("Found");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        if (!hit)
        {
            Gizmos.color = Color.red;
            Vector2 direction = transform.right * 5;
            Gizmos.DrawRay(this.transform.position, direction);
        }
    }

    
}
