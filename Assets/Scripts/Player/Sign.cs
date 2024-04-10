using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public GameObject signSprite;
    private IInteractable targetItem;//获得与玩家碰撞的可互动的物体的接口

    private bool canPress;//判断是否可交互

    public Transform PlayerTrans;
    private void Awake()
    {
        inputControl = new PlayerInputControl();
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    private void Update()
    {
        signSprite.SetActive(canPress);
        signSprite.transform.localScale = PlayerTrans.localScale;
        inputControl.Gameplay.Confirm.started += OnConfirm;
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (canPress)
            targetItem.TiggerAction();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable")){ 
            canPress = true;
            targetItem = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }

}
