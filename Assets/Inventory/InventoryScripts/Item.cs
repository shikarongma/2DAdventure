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
    public string itemName;//物体名字
    public Sprite itemImage;//物体图片
    public int itemHeld;//物体数量
    [TextArea]
    public string itemInfo;//物体描述
    public bool Equip;//物体是否可装备


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
