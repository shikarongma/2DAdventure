using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventroy/New Item")]
public class Item : ScriptableObject,IItemEfficacy
{
    [HideInInspector] public GameObject Player;
    public string itemName;//��������
    public Sprite itemImage;//����ͼƬ
    public int itemHeld;//��������
    [TextArea]
    public string itemInfo;//��������
    public bool Equip;//�����Ƿ��װ��


    public void achieveFunction()
    {
        switch (itemName)
        {
            case "Shoe":
                Player.GetComponent<PlayerController>().ShoeFunction();
                break;
            case "Sword":
                for(int i=0;i<3;i++)
                    Player.transform.GetChild(0).GetChild(i).GetComponent<Attack>().SwordFunction();
                break;
        }
    }

}
