using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    //�㲥
    public PlayAudioEventSO palyAudioEvent;
    public AudioClip audioClip;
    public bool playOnEnable;//�Ƿ�һ��ʼ�Ͳ���

    private void OnEnable()
    {
        if (playOnEnable)
            PlayAudioClip();
    }
    public void PlayAudioClip()
    {
        palyAudioEvent.RaiseEvent(audioClip);
    }
}
