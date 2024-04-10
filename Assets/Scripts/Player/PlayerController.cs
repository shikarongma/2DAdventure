using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //������Ϸ�����¼������¼���������Ҳ����ڲٿ�player
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
    //��Ծ��Ч
    public PlayAudioEventSO JumpEvents;
    public AudioClip audioClip;

    [Header("��������")]
    //�˶��ٶ�
    public float speed;
    public float jumpForce;
    public float hurtForce;
    //���ι�������
    //public int combo;

    [Header("�������")]

    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("״̬")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool isOpen;
    public bool isEnding;
    //�Ƿ�������
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
        //��Ծ
        inputControl.Gameplay.Jump.started += Jump;

        //����
        inputControl.Gameplay.Attack.started += PlayerAttack;
        //�򿪱���
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

    //�����ƶ�ͨ����FixedUpdate�е���
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

        //���﷭ת
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

    //��鵱ǰ�����״̬ �ڵ�������normal������� ���������wall�������
    private void CheckState()
    {
        coll.sharedMaterial = physicscheck.isGround ? normal : wall;
    }

    //���������
    private void OpenMyBag(InputAction.CallbackContext obj)
    {
        isOpen = !isOpen;
        if (!isOpen)
        {
            UseButton.SetActive(false);
            InventoryManager.UpdateItemInfo("");//�رձ�����ɾ����������ϵ�����
        }
        myBag.SetActive(isOpen);
    }

    public void ShoeFunction()//Ь��ʵ�ֹ���
    {
        float currentSpeed = speed;
        speed = (float) (1 + 0.1) * currentSpeed;//���ٶ���߰ٷ�֮ʮ ά��ʮ��
        speedUpFinish = false;
        StartCoroutine(IEtimer(currentSpeed));
    }
    
    //Э��
    IEnumerator IEtimer(float cuurentSpeed)
    {
        yield return new WaitForSeconds(30.0f);
        speed = cuurentSpeed;
    }
}
