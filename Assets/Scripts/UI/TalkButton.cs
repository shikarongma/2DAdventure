using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject TalkUI;
    public DialogSystem dialog;
    public PlayerInputControl inputControl;


    private void Awake()
    {
        inputControl = new PlayerInputControl();
    }
    private void Update()
    {
        inputControl.Gameplay.TalkUI.started += OpenorCloseTalkUI;
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void OpenorCloseTalkUI(InputAction.CallbackContext obj)
    {
        if (Button.activeSelf)
        {
            TalkUI.SetActive(true);
        }
        else
        {
            TalkUI.SetActive(false);
            dialog.index = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       Button.SetActive(true);

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
    }
    
}