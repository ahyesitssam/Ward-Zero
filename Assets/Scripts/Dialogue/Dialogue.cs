using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [Header("Content")]
    public string[] speech;
    public string[] characters;

    [Header("Settings")]
    [Range(0.001f, 0.5f)]
    public float typingSpeed;
    [Range(0.5f, 10)]
    public float nextSpeechSpeed;

    [Header("Requirements")]
    public Text speechBox;
    public Text characterBox;
    public Image dialoguePanel;
    public bool playOnStart;

    private void Start()
    {
        if (playOnStart == true)
        {
            StartCoroutine(Talk(speech));
        } else if (playOnStart == false)
        {
            characterBox.enabled = false;
            speechBox.enabled = false;
            dialoguePanel.enabled = false;
        }
    }

    public IEnumerator Talk(string[] text)
    {
        characterBox.enabled = true;
        speechBox.enabled = true;
        dialoguePanel.enabled = true;

        for (int o = 0; o < text.Length; o++)
        {
            characterBox.text = characters[o];
            for (int i = 0; i < text[o].Length + 1; i++)
            {
                string currentText = text[o].Substring(0, i);
                speechBox.text = currentText;
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitForSecondsRealtime(nextSpeechSpeed);
        }

        characterBox.enabled = false;
        speechBox.enabled = false;
        dialoguePanel.enabled = false;
    }
}
