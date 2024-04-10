using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    //广播
    public PlayAudioEventSO palyAudioEvent;
    public AudioClip audioClip;
    public bool playOnEnable;//是否一开始就播放

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
