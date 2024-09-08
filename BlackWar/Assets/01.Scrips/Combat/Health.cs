using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    protected readonly int HASH_DEAD = Animator.StringToHash("Dead");

    protected Army _owner;
    protected Enemy _enemyOwner;

    protected Animator _anim;
    protected Collider2D _collider;

    public int playerMaxHealth;
    public int enemyMaxHealth;
    public int playerCurrentHealth;
    public int enemyCurrentHealth;
    public bool IsDead = false;

    private PlayerStat _playerStat;
    private EnemyStat _enemyStat;

    protected virtual void Awake()
    {
        _owner = GetComponent<Army>();
        _enemyOwner = GetComponent<Enemy>();
        _collider = GetComponent<Collider2D>();
        _anim = transform.Find("Visual").GetComponent<Animator>();
    }

    public void PlayerSetHealth(PlayerStat onwer)
    {
        _playerStat = onwer;
        playerCurrentHealth = playerMaxHealth = (int)onwer.MaxHp.GetValue();
    }

    public void EnemySetHealth(EnemyStat onwer)
    {
        _enemyStat = onwer;
        enemyCurrentHealth = enemyMaxHealth = (int)onwer.MaxHp.GetValue();
    }

    public void ArmyApplyDamage(int damage)
    {
        playerCurrentHealth -= damage;

        if (playerCurrentHealth < 0)
        {
            OnArmyDied();
        }
    }

    public void EnemyApplyDamage(int damage)
    {
        enemyCurrentHealth -= damage;

        if (enemyCurrentHealth < 0)
        {
            OnEnemyDied();
        }
    }

    public void OnArmyDied()
    {
        // ��� bool ������ ��ȸ�ϸ� ��Ȱ��ȭ�ϱ�
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        _anim.speed = 1f;

        //�״� �ִϸ��̼� ó��
        _anim.SetBool(HASH_DEAD, true);
        _collider.enabled = false;
        //��ƼƼ ���� ó��
        _owner.IsDead = true;
        //��ƼƼ ��ũ��Ʈ ����
        _owner.enabled = false;
    }

    public void OnEnemyDied()
    {
        // ��� bool ������ ��ȸ�ϸ� ��Ȱ��ȭ�ϱ�
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        _anim.speed = 1f;

        //�״� �ִϸ��̼� ó��
        _anim.SetBool(HASH_DEAD, true);
        _collider.enabled = false;
        //��ƼƼ ���� ó��
        _enemyOwner.IsDead = true;
        //��ƼƼ ��ũ��Ʈ ����
        _enemyOwner.enabled = false;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
