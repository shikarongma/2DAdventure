using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivationUseButton : MonoBehaviour
{
    public Inventory MyBag;//��ȡ�ҵı���
    private Item currentitem;//��ǰ�����Ʒ

    //�õ���ǰ����use��ť��item
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
        int num = -1;//��¼��һ���Ǹ���Ʒ���Ա㽫���Ϊnull
        foreach (var item2 in MyBag.itemList)
        {
            num++;
            if (item2 == currentitem)
            {
                item2.achieveFunction();
                item2.itemHeld--;
                if (item2.itemHeld == 0)//ʹ�ú�û�и�itemʱ
                {
                    MyBag.itemList[num] = null;
                    gameObject.SetActive(false);//�ر�useButton
                    InventoryManager.UpdateItemInfo("");//ɾ����������ϵ�����

                }
                InventoryManager.RefreshItem();
                return;
            }
        }

    }
}
