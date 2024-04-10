using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //拖拽接口实现
    //原始父级
    public Transform originalParent;//当前物体的位置transform
    public Inventory myBag;
    private int currentItemId;//当前物体的id
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;//当前的transform是Item
        currentItemId = originalParent.GetComponent<Slot>().slotId;
        //transform.parent = transform.parent.parent;不可取 容易出bug
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;//射线阻挡关闭
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);//输出鼠标当前位置下到第一个碰到物体的名字
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //里面有物品
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")//判断下面物体名字是：Item Image 那么互换位置
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //itmeList（背包）的物品存储位置改变
                var temp = myBag.itemList[currentItemId];
                myBag.itemList[currentItemId] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>().slotId];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>().slotId] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                //最好不用这种方法 容易出bug
                //eventData.pointerCurrentRaycast.gameObject.transform.parent.parent = originalParent;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中移动的物品
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                //里面没有物品 就直接挂载到slot上
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //itmeList（背包）的物品存储位置改变
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<Slot>().slotId] = myBag.itemList[currentItemId];
                //解决自己放在自己位置的问题
                if (eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<Slot>().slotId != currentItemId)
                    myBag.itemList[currentItemId] = null;

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        //其他任何位置都归位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
