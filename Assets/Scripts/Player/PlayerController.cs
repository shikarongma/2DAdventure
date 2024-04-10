using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //接受游戏结束事件，若事件发生，玩家不可在操控player
    public VoidEventSO gameOverEvent;
    public GameObject UseButton;

    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Vector2 inputDirection;
    private PhysicsCheck physicscheck;
    private PlayerAnimation playeranimation;
    private CapsuleCollider2D coll;
    public GameObject myBag;
    //跳跃音效
    public PlayAudioEventSO JumpEvents;
    public AudioClip audioClip;

    [Header("基本参数")]
    //运动速度
    public float speed;
    public float jumpForce;
    public float hurtForce;
    //三段攻击计数
    //public int combo;

    [Header("物理材质")]

    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool isOpen;
    public bool isEnding;
    //是否加速完成
    public bool speedUpFinish;

    //Awake>OnEnable>Start
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        physicscheck = GetComponent<PhysicsCheck>();
        playeranimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();

        inputControl = new PlayerInputControl();
        //跳跃
        inputControl.Gameplay.Jump.started += Jump;

        //攻击
        inputControl.Gameplay.Attack.started += PlayerAttack;
        //打开背包
        inputControl.Gameplay.MyBag.started += OpenMyBag;

    }



    private void OnEnable()
    {
        inputControl.Enable();
        gameOverEvent.OnEventRaised += OnRaisedEvent;
    }

    private void OnDisable()
    {
        inputControl.Disable();
        gameOverEvent.OnEventRaised -= OnRaisedEvent;
    }

    private void OnRaisedEvent()
    {
        isEnding = true;
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();

        CheckState();
    }

    //产生移动通常在FixedUpdate中调用
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack && !isEnding)
            Move();
        else if (isEnding)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log(other.name);
    //}

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //人物翻转
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = 1;
        //sr.flipX = false;
        if (inputDirection.x < 0)
            faceDir = -1;
        //sr.flipX = true;
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("JUMP");
        if (physicscheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            JumpEvents.RaiseEvent(audioClip);
        }
    }

    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        if (physicscheck.isGround)
        {
            playeranimation.PlayAttack();
            isAttack = true;
        }

    }

    #region UnityEvent
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
    #endregion

    //检查当前人物的状态 在地上则用normal物理材质 跳起后则用wall物理材质
    private void CheckState()
    {
        coll.sharedMaterial = physicscheck.isGround ? normal : wall;
    }

    //背包开与关
    private void OpenMyBag(InputAction.CallbackContext obj)
    {
        isOpen = !isOpen;
        if (!isOpen)
        {
            UseButton.SetActive(false);
            InventoryManager.UpdateItemInfo("");//关闭背包，删除背包面板上的描述
        }
        myBag.SetActive(isOpen);
    }

    public void ShoeFunction()//鞋子实现功能
    {
        float currentSpeed = speed;
        speed = (float) (1 + 0.1) * currentSpeed;//将速度提高百分之十 维持十秒
        speedUpFinish = false;
        StartCoroutine(IEtimer(currentSpeed));
    }
    
    //协程
    IEnumerator IEtimer(float cuurentSpeed)
    {
        yield return new WaitForSeconds(30.0f);
        speed = cuurentSpeed;
    }
}
