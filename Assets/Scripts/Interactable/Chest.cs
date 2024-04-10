using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;//获得组件，更换宝箱图片
    public Sprite openSprite;//打开的图片
    public Sprite closeSprite;//关闭的图片

    public GameObject sign;//获取玩家可点击按钮
    public bool isDone;//状态

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? openSprite : closeSprite;
    }
    public void TiggerAction()
    {
        if (!isDone)
        {
            OpenChest();
            StartCoroutine(destroySign());
        }
    }

    private IEnumerator destroySign()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    private void OpenChest()
    {
        spriteRenderer.sprite = openSprite;
        isDone = true;
        gameObject.tag = "Untagged";
        GetComponent<AudioDefination>().PlayAudioClip();
    }
}
