using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTriggerGertrude : MonoBehaviour
{
    private DialogueManager DM;
    void Start()
    {
        DM = GameObject.Find("Dialogue System").GetComponent<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player talking to Gertrude");
            DM.GertrudeFirstMeet();
        }
    }
}
