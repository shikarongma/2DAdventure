using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;
    public float currrentHealth;

    [Header("�����޵�")]//����ʱ����
    //�޵�ʱ��
    public float invulnerableDuration;
    //��ǰʱ��
    private float invulnerableCounter;
    //�Ƿ��޵�
    public bool invulnerable=false;
    //�¼�
    //public CharacterEventSO OnHealthChange;//�����У���ΪenemyҲ�д˴���
    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
                invulnerable = false;
        }
    }
    private void Start()
    {
        currrentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
        //OnHealthChange.RaiseEvent(this);
    }
    //�����˺�
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
            return;
        //Debug.Log(attacker.damage);
        if (currrentHealth - attacker.damage > 0)
        {
            currrentHealth -= attacker.damage;
            TriggerInvulnerable();
            //ִ������
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currrentHealth = 0;
            //��������
            OnDie?.Invoke();
        }
        OnHealthChange?.Invoke(this);
        //OnHealthChange.RaiseEvent(this);
    }

    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
