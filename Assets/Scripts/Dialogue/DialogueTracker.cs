using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTracker : MonoBehaviour
{
    [Header("NPC Info")]
    [SerializeField] private string npcName;
    [SerializeField] private Image[] npcPortraits;

    [Header("Dialogue Settings")]
    [Range(0.001f, 0.5f)]
    [SerializeField] private float typingSpeed = 0.05f;
    [Range(0.5f, 10)]
    [SerializeField] private float nextSpeechSpeed = 1f;

    [Header("UI References")]
    [SerializeField] private Text speechBox;
    [SerializeField] private Text characterBox;
    [SerializeField] private Image dialoguePanel;

    protected virtual void Start()
    {
        HideDialogueUI();
    }

    private IEnumerator Talk(string[] dialogueLines)
    {
        ShowDialogueUI();

        for (int i = 0; i < dialogueLines.Length; i++)
        {
            for (int j = 0; j <= dialogueLines[i].Length; j++)
            {
                speechBox.text = dialogueLines[i].Substring(0, j);
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        HideDialogueUI();
    }

    private void ShowDialogueUI()
    {
        characterBox.text = npcName;
        characterBox.enabled = true;
        speechBox.enabled = true;
        dialoguePanel.enabled = true;
    }

    private void HideDialogueUI()
    {
        characterBox.enabled = false;
        speechBox.enabled = false;
        dialoguePanel.enabled = false;
    }

    public void TriggerDialogue()
    {
        //Commented this out so unity doesn't yell errors at me -James
        //StartCoroutine(Talk());
    }
}
