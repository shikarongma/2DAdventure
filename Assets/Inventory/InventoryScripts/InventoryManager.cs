using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory myBag;
    //public Slot slotPrefab;
    public GameObject emptySlot;
    public GameObject slotGrid;
    public Text itemInfromation;

    public List<GameObject> slots = new List<GameObject>();
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInfromation.text = "";
    }

    //面板上展示物品描述
    public static void UpdateItemInfo(string itemInfromation)
    {
        instance.itemInfromation.text = itemInfromation;
    }

    //public static void CreatNewItem(Item item)
    //{
    //    Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
    //    newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
    //    newItem.itemSlot = item;
    //    newItem.numSlot.text = item.itemHeld.ToString();
    //    newItem.imageSlot.sprite = item.itemImage;
    //}

    public static void RefreshItem()
    {
        //循环删除slotGrid下的子集物体
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        //重新生成对应myBag里面的物品slot
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            //CreatNewItem(instance.myBag.itemList[i]);
            instance.slots.Add(Instantiate(instance.emptySlot,instance.slotGrid.transform));
            //instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().slotId = i;
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);
        }
    }
}
