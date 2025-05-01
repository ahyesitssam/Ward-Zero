using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager DM;
    void Start()
    {
        DM = GameObject.Find("Dialogue System").GetComponent<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
