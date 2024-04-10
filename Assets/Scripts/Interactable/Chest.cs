using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;//����������������ͼƬ
    public Sprite openSprite;//�򿪵�ͼƬ
    public Sprite closeSprite;//�رյ�ͼƬ

    public GameObject sign;//��ȡ��ҿɵ����ť
    public bool isDone;//״̬

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
