using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;

    public GameObject gameOverUI;

    [Header("�¼�����")]
    public CharacterEventSO healthEvent;

    //����UI
    public VoidEventSO gameOverEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        gameOverEvent.OnEventRaised += OnRaiseEvent;
    }

   

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        gameOverEvent.OnEventRaised -= OnRaiseEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.currrentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
    }

    private void OnRaiseEvent()
    {
        gameOverUI.gameObject.SetActive(true);
    }
}
