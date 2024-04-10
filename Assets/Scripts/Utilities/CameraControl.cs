using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;
    //�����
    public CinemachineImpulseSource impulseSource;
    public VoidEventSO cameraShakeEvent;

    //�¼�����
    public VoidEventSO AfterSceneLoadedEvent;

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

   // �����л������
    private void Start()
    {
        GetNewCameraBounds();
    }

    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
        AfterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
        AfterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }
    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    private void OnAfterSceneLoadedEvent()
    {
        GetNewCameraBounds();
    }

    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
            return;

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        confiner2D.InvalidateCache();
    }
}
