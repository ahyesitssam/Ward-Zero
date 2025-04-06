using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private InventoryMenu IM;
    [SerializeField] int inventoryID;

    public void addToInventory()
    {
        
        if (IM.IsInventoryFull() != true)
        {
            IM.AddItem(inventoryID);
            Object.Destroy(this.gameObject);
        }
        else
        {
            //flash inventory full on screen or smth
        }
    }

    void Start()
    {
        IM = GameObject.Find("Canvas").GetComponent<InventoryMenu>(); //IM = InventoryMenu
    }

}
