using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class backNforth : MonoBehaviour
{
    [SerializeField] float min = 2f;
    [SerializeField] float max = 3f;
    RaycastHit2D hit;
    bool enemySummoned = false;
    // Use this for initialization
    void Start()
    {

        min = transform.position.y;
        max = transform.position.y + 3;
        
    }

    // Update is called once per frame
    void Update()
    {


        

        if (!hit)
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min), transform.right , 5.0f); //Change the Vector2.left to whatever direction we want default forward to be.
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z);
            //agent.SetDestination(new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z));
        }



        if (hit && hit.collider.tag == "Player" && hit.collider.tag != null && !enemySummoned)
        {
            Debug.Log("summonEnemy");
            enemySummoned = true;
        }
        else 
        {
            hit = Physics2D.Raycast(new Vector2(this.transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min), transform.right, 5.0f);
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z);
        }

    }
}
