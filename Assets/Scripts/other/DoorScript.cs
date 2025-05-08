using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private ActionTracker AT;
    bool awesomeBoolean = false;

    void Start()
    {
        AT = GameObject.Find("Canvas").GetComponent<ActionTracker>();
    }

    void Update()
    {
        if(AT != null && AT.actionAmount == 0)
        {
            gameObject.SetActive(false);
            awesomeBoolean = true;
        }else if (awesomeBoolean)
        {
            gameObject.SetActive(false);
        }
    }
}
