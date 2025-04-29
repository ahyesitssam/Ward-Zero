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
        Debug.Log(IM.checkForItem(1));
    }
}
