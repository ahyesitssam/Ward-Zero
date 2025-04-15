using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animation anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.Play("BackwardsWalk");
        }

    }
}
