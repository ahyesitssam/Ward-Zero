using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testsc : MonoBehaviour
{
    private InventoryMenu IM;

    void Start()
    {
        IM = GameObject.Find("Canvas").GetComponent<InventoryMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)){
            Debug.Log("Wood:" + IM.checkForItem(1));
            Debug.Log("Rock:" + IM.checkForItem(2));
            Debug.Log("Syr:" + IM.checkForItem(3));
            Debug.Log("Mask:" + IM.checkForItem(4));
            Debug.Log("Pills:" + IM.checkForItem(5));
            Debug.Log("Tank:" + IM.checkForItem(6));
        }
    }
}
