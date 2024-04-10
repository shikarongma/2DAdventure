using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item itemSlot;
    public Image imageSlot;
    public Text numSlot;
    public int slotId;//在面板上的编号

    public GameObject ItemInSlot;
    //点击物品，面板上展示物品的描述
    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(itemSlot.itemInfo);
        //点击物品时。use按钮才出现。通过获取当前item的父级的父级，在或得其button子级
        this.gameObject.transform.parent.parent.GetChild(4).gameObject.SetActive(true);
        this.gameObject.transform.parent.parent.GetChild(4).gameObject.GetComponent<ActivationUseButton>().FindItem(itemSlot);
    }

    public void SetupSlot(Item item)
    {
        if (item == null)
        {
            ItemInSlot.SetActive(false);
            return;
        }

        itemSlot = item;
        imageSlot.sprite = item.itemImage;
        numSlot.text = item.itemHeld.ToString();
    }
}
