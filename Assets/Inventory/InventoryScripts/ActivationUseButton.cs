using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivationUseButton : MonoBehaviour
{
    public Inventory MyBag;//获取我的背包
    private Item currentitem;//当前点击物品

    //得到当前激活use按钮的item
    private void Update()
    {
        
    }
    public void FindItem(Item item)
    {
        currentitem = item;
    }
    public void ActuvationUse()
    {
        Debug.Log(currentitem.name);
        int num = -1;//记录哪一个是该物品，以便将其变为null
        foreach (var item2 in MyBag.itemList)
        {
            num++;
            if (item2 == currentitem)
            {
                item2.achieveFunction();
                item2.itemHeld--;
                if (item2.itemHeld == 0)//使用后没有该item时
                {
                    MyBag.itemList[num] = null;
                    gameObject.SetActive(false);//关闭useButton
                    InventoryManager.UpdateItemInfo("");//删除背包面板上的描述

                }
                InventoryManager.RefreshItem();
                return;
            }
        }

    }
}
