using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory myBag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thisItem.Player = other.gameObject;
            AddNewItem();
            Destroy(gameObject);
        }
    }

    private void AddNewItem()
    {
        thisItem.itemHeld++;

        if (!myBag.itemList.Contains(thisItem))
        {
            //playerInventory.itemList.Add(thisItem);
            for (int i = 0; i < myBag.itemList.Count; i++)
            {
                if (myBag.itemList[i] == null)
                {
                    myBag.itemList[i] = thisItem;
                    break;
                }
            }
        }
        InventoryManager.RefreshItem();
    }
}
