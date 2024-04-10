using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item itemSlot;
    public Image imageSlot;
    public Text numSlot;
    public int slotId;//������ϵı��

    public GameObject ItemInSlot;
    //�����Ʒ�������չʾ��Ʒ������
    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(itemSlot.itemInfo);
        //�����Ʒʱ��use��ť�ų��֡�ͨ����ȡ��ǰitem�ĸ����ĸ������ڻ����button�Ӽ�
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
