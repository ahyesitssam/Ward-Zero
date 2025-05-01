using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private string npcName;
    [SerializeField] private GameObject playerPortrait;
    [SerializeField] private GameObject gertrudePortrait;
    [SerializeField] private GameObject HaroldPortrait;
    [SerializeField] private GameObject LillyPortrait;

    private int playerCount = 0;
    private int gertrudeCount = 0;
    private int haroldCount = 0;
    private int lillyCount = 0;

    [Header("Lines")]
    public string[] playerStartLines;
    public string[] gertrudeLines;
    public string[] haroldLines;
    public string[] lillyLines;

    [Header("Dialogue Settings")]
    [Range(0.001f, 0.5f)]
    [SerializeField] private float typingSpeed = 0.05f;

    [Header("UI References")]
    [SerializeField] private Text speechBox;
    [SerializeField] private Text characterBox;
    [SerializeField] private GameObject dialoguePanel;

    protected virtual void Start()
    {
        HideDialogueUI();
    }

    private IEnumerator Talk(string[] dialogueLines)
    {
        ShowDialogueUI();

        Debug.Log("Text being displayed");

        for (int i = 0; i < dialogueLines.Length; i++)
        {
            for (int j = 0; j <= dialogueLines[i].Length; j++)
            {
                speechBox.text = dialogueLines[i].Substring(0, j);
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        HideDialogueUI();
    }

    private void ShowDialogueUI()
    {
        Debug.Log("Dialog UI Shown");
        characterBox.text = npcName;
        dialoguePanel.SetActive(true);
        
    }

    private void HideDialogueUI()
    {
        Debug.Log("Dialog UI Hidden");
        dialoguePanel.SetActive(false);

        playerPortrait.SetActive(false);
        gertrudePortrait.SetActive(false);
        HaroldPortrait.SetActive(false);
        LillyPortrait.SetActive(false);
    }

    public void playerTalkStartGame()
    {
        npcName = "Me";
        playerPortrait.SetActive(true);
        StartCoroutine(Talk(playerStartLines));
        playerCount++;
    }
}
