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

    [Header("Harold Lines")]
    public string[] playerHaroldFirstMeet;
    public string[] HaroldFirstMeet;
    public string[] haroldWaitForItem;
    public string[] playerGiveHaroldItem;

    [Header("Lilly Lines")]
    public string[] playerLillyFirstMeet;
    public string[] LillyFirstMeet;
    public string[] LillyWaitForItem;
    public string[] playerGiveLillyItem;

    [Header("Gertrude Lines")]
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
    private ActionTracker AT;
    [SerializeField] private string[] playerBasementLines;

    #region UI/Talk

    protected virtual void Start()
    {
        AT = GameObject.Find("Canvas").GetComponent<ActionTracker>();
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

    #region Lilly

    public void GiveLillyItem()
    {
        npcName = "Lilly";
        LillyPortrait.SetActive(true);
        StartCoroutine(LillyEnd());
    }

    public void LillyWaitForPills()
    {
        npcName = "Lilly";
        LillyPortrait.SetActive(true);
        StartCoroutine(Talk(LillyWaitForItem));
    }

    public void LillyFirstMeetPlayer()
    {
        StartCoroutine(LillyFirstMeetSequence());
    }

    private IEnumerator LillyFirstMeetSequence()
    {
        npcName = "Lilly";
        LillyPortrait.SetActive(true);
        yield return StartCoroutine(Talk(LillyFirstMeet));

        npcName = "Me";
        playerPortrait.SetActive(true);
        yield return StartCoroutine(Talk(playerLillyFirstMeet));
    }

    private IEnumerator LillyEnd()
    {
        yield return StartCoroutine(Talk(playerGiveLillyItem));

        if (AT.actionAmount <= 0)
        {
            BasementTrigger();
        }
    }


    #endregion


    #region Harold

    public void HaroldWaitForSandwich()
    {
        npcName = "Harold";
        HaroldPortrait.SetActive(true);
        StartCoroutine(Talk(haroldWaitForItem));
    }

    public void giveHaroldItem()
    {
        npcName = "Harold";
        HaroldPortrait.SetActive(true);
        StartCoroutine(HaroldEnd());
    }

    private IEnumerator HaroldEnd()
    {
        yield return StartCoroutine(Talk(playerGiveHaroldItem));

        if (AT.actionAmount <= 0)
        {
            BasementTrigger();
        }
    }

    public void HaroldFirstMeetPlayer()
    {
        StartCoroutine(HaroldFirstMeetSequence());
    }

    private IEnumerator HaroldFirstMeetSequence()
    {
        npcName = "Me";
        playerPortrait.SetActive(true);
        yield return StartCoroutine(Talk(playerHaroldFirstMeet));

        npcName = "Harold";
        HaroldPortrait.SetActive(true);
        yield return StartCoroutine(Talk(HaroldFirstMeet));
    }

    #endregion

    #region Player

    public void playerTalkStartGame()
    {
        npcName = "Me";
        playerPortrait.SetActive(true);
        StartCoroutine(Talk(playerStartLines));
    }

    private void BasementTrigger()
    {
        npcName = "Me";
        playerPortrait.SetActive(true);
        StartCoroutine(Talk(playerBasementLines));
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

        if (AT.actionAmount <= 0)
        {
            BasementTrigger();
        }
    }




    #endregion
}
