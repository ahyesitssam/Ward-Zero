using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    public string[] dialogueLines;

    public NPC(string name, string[] lines)
    {
        npcName = name;
        dialogueLines = lines;
    }
     

}
