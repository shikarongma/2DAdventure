using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //��ק�ӿ�ʵ��
    //ԭʼ����
    public Transform originalParent;//��ǰ�����λ��transform
    public Inventory myBag;
    private int currentItemId;//��ǰ�����id
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;//��ǰ��transform��Item
        currentItemId = originalParent.GetComponent<Slot>().slotId;
        //transform.parent = transform.parent.parent;����ȡ ���׳�bug
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;//�����赲�ر�
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);//�����굱ǰλ���µ���һ���������������
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //��������Ʒ
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")//�ж��������������ǣ�Item Image ��ô����λ��
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //itmeList������������Ʒ�洢λ�øı�
                var temp = myBag.itemList[currentItemId];
                myBag.itemList[currentItemId] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>().slotId];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>().slotId] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                //��ò������ַ��� ���׳�bug
                //eventData.pointerCurrentRaycast.gameObject.transform.parent.parent = originalParent;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;//�����赲��������Ȼ�޷��ٴ�ѡ���ƶ�����Ʒ
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                //����û����Ʒ ��ֱ�ӹ��ص�slot��
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //itmeList������������Ʒ�洢λ�øı�
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<Slot>().slotId] = myBag.itemList[currentItemId];
                //����Լ������Լ�λ�õ�����
                if (eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<Slot>().slotId != currentItemId)
                    myBag.itemList[currentItemId] = null;

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        //�����κ�λ�ö���λ
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
