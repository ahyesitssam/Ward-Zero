using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] Transform target;
    NavMeshAgent agent;
    RaycastHit2D hit;

    [SerializeField] bool left = false;
    [SerializeField] bool right = false;
    [SerializeField] bool up = false;
    [SerializeField] bool down = false;
    

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
        if (!hit && right)
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), transform.right, 5.0f); //Change the Vector2.left to whatever direction we want default forward to be.
            //Debug.Log("checking");
        }
        else if (!hit && left)
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), -transform.right, 5.0f);
        }
        else if (!hit && up)
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), transform.up, 5.0f);
        }
        else if (!hit && down) 
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), -transform.up, 5.0f);
        }


        if (hit && hit.collider.tag == "Player") 
        {
            agent.speed = 5.0f;
            Debug.Log("Found");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Draws a 5 unit long red line in front of the object
        if (!hit)
        {
            
            
        }

        if (!hit && right)
        {
            Vector2 direction = transform.right * 5;
            Gizmos.DrawRay(this.transform.position, direction);
        }
        else if (!hit && left)
        {
            Vector2 direction = -transform.right * 5;
            Gizmos.DrawRay(this.transform.position, direction);
        }
        else if (!hit && up)
        {
            Vector2 direction = transform.up * 5;
            Gizmos.DrawRay(this.transform.position, direction);
        }
        else if (!hit && down)
        {
            Vector2 direction = -transform.up * 5;
            Gizmos.DrawRay(this.transform.position, direction);
        }
    }

    
}
