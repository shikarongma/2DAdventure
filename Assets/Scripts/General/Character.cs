using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currrentHealth;

    [Header("受伤无敌")]//（计时器）
    //无敌时间
    public float invulnerableDuration;
    //当前时间
    private float invulnerableCounter;
    //是否无敌
    public bool invulnerable=false;
    //事件
    //public CharacterEventSO OnHealthChange;//不可行，因为enemy也有此代码
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
    //接受伤害
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
            return;
        //Debug.Log(attacker.damage);
        if (currrentHealth - attacker.damage > 0)
        {
            currrentHealth -= attacker.damage;
            TriggerInvulnerable();
            //执行受伤
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currrentHealth = 0;
            //触发死亡
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
