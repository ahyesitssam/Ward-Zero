using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTracker : MonoBehaviour
{
    public int actionAmount = 3; // action tracking

    [SerializeField] public Sprite[] actionAmountImages; // the images of amount of actions
    [SerializeField] public GameObject actionBar; //the UI that shows how many actions player has

    [SerializeField] public GameObject basementCollider;

    private DialogueManager DM;


    void Start()
    {
        //probably connect to the game manager?
        DontDestroyOnLoad(this.gameObject);
        DM = GameObject.Find("Dialogue System").GetComponent<DialogueManager>();
    }

    void Update()
    {
        checkActionAmount();
        updateActionImage();
        /*if (Input.GetKeyDown(KeyCode.H))//for testing
        {
            actionAmount--;
        }*/
    }

    public void checkActionAmount()
    {
        if(actionAmount <= 0)
        {
            actionBar.SetActive(false);
            actionAmount = 0;
            //out of actions time advances

            DM.BasementTrigger();
        }
    }

    public void updateActionImage()
    {
        actionBar.GetComponent<Image>().sprite = actionAmountImages[actionAmount]; //updates the sprites
    }

    public void useAnAction() //called from other files on things that will use actions to lower the total
    {
        actionAmount--;
        checkActionAmount();
    }
}
