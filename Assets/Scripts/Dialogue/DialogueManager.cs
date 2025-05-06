using System;
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

    [Header("Lines")]
    public string[] playerStartLines;
    public string[] gertrudeFirstMeetLines;
    public string[] playerGertrudeFirstMeetLines;
    public string[] gertrudeWaitForItem;
    public string[] playerGiveGertrudeItem;
    public string[] gertrudeTakesItem;


    [Header("Dialogue Settings")]
    [Range(0.001f, 0.5f)]
    [SerializeField] private float typingSpeed = 0.05f;

    [Header("UI References")]
    [SerializeField] private Text speechBox;
    [SerializeField] private Text characterBox;
    [SerializeField] private Text pressEnterText;
    [SerializeField] private GameObject dialoguePanel;
    private string enterText = "Press enter to continue...";

    private Player P;

    #region UI/Talk

    protected virtual void Start()
    {
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
        pressEnterText.text = "";
        dialoguePanel.SetActive(false);

        playerPortrait.SetActive(false);
        gertrudePortrait.SetActive(false);
        HaroldPortrait.SetActive(false);
        LillyPortrait.SetActive(false);
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

            StartCoroutine(displayEnterText());

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            pressEnterText.text = "";
        }

        HideDialogueUI();
    }

    private IEnumerator displayEnterText()
    {
        for (int i = 0; i <= enterText.Length; i++)
        {
            pressEnterText.text = enterText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }

    }


    #endregion


    #region Player

    public void playerTalkStartGame()
    {
        npcName = "Me";
        playerPortrait.SetActive(true);
        StartCoroutine(Talk(playerStartLines));
    }

    #endregion

    #region Gertrude

    public void GertrudeFirstMeet()
    {
        StartCoroutine(GertrudeFirstMeetSequence());
    }

    private IEnumerator GertrudeFirstMeetSequence()
    {
        npcName = "Gertrude";
        gertrudePortrait.SetActive(true);
        yield return StartCoroutine(Talk(gertrudeFirstMeetLines));

        npcName = "Me";
        playerPortrait.SetActive(true);
        yield return StartCoroutine(Talk(playerGertrudeFirstMeetLines));
    }

    public void GertrudeWaitingOnItem()
    {
        npcName = "Gertrude";
        gertrudePortrait.SetActive(true);
        StartCoroutine(Talk(gertrudeWaitForItem));
    }

    public void GiveGertrudeOxygen()
    {
        StartCoroutine(GiveGertrudeOxygenSequence());
    }

    private IEnumerator GiveGertrudeOxygenSequence()
    {
        npcName = "Me";
        playerPortrait.SetActive(true);
        yield return StartCoroutine(Talk(playerGiveGertrudeItem));

        npcName = "Gertrude";
        gertrudePortrait.SetActive(true);
        yield return StartCoroutine(Talk(gertrudeTakesItem));

    }


    #endregion
}
