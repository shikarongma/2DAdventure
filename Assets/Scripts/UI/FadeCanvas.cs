using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FadeCanvas : MonoBehaviour
{
    //���뽥��ͼƬ
    public Image fadeImage;

    [Header("�¼�����")]
    public FadeCanvasSO fadeEvent;

    private void OnEnable()
    {
        fadeEvent.OnEventRaised += OnFadeEvent;
    }

    private void OnDisable()
    {
        fadeEvent.OnEventRaised -= OnFadeEvent;
    }

    private void OnFadeEvent(Color target, float duration)
    {
        fadeImage.DOBlendableColor(target, duration);
    }
}
