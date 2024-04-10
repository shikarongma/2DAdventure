using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeCanvasSO")]
public class FadeCanvasSO : ScriptableObject
{
    //���뽥�뽥������ɫ�Լ�����ʱ��
    public UnityAction<Color, float> OnEventRaised;

    //���
    public void FadeIn(float duration)
    {
        RaisedEvent(Color.black, duration);
    }

    //��͸��
    public void FadeOut(float duration)
    {
        RaisedEvent(Color.clear, duration);
    }

    public void RaisedEvent(Color target,float duration)
    {
        OnEventRaised?.Invoke(target, duration);
    }
}
