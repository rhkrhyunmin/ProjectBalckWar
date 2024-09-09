using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    protected readonly int HASH_DEAD = Animator.StringToHash("Dead");

    protected Army _owner;

    protected Animator _anim;
    protected NavMeshAgent _agent;
    protected Collider _collider;

    public int playerMaxHealth;
    public int enemyMaxHealth;
    public int playerCurrentHealth;
    public int enemyCurrentHealth;
    public int armyCastleMaxHealth;
    public int enemyCastleMaxHealth;
    public int armyCastleCurrentHealth;
    public int enemyCastleCurrentHealth;

    private PlayerStat _playerStat;
    private EnemyStat _enemyStat;
    private CastleSO _playercastleStat;
    private EnemyCastleSo _enemyCastleStat;

    public bool IsDead;

    protected virtual void Awake()
    {
        _owner = GetComponent<Army>();
        _collider = GetComponent<Collider>();
        _anim = transform.Find("Visual").GetComponent<Animator>();
    }

    public void PlayerSetHealth(PlayerStat onwer)
    {
        _playerStat = onwer;
        playerCurrentHealth = playerMaxHealth = (int)onwer.MaxHp.GetValue();

        if(playerCurrentHealth < 0)
        {
            OnDied();
        }
    }

    public void EnemySetHealth(EnemyStat onwer)
    {
        _enemyStat = onwer;
        enemyCurrentHealth = enemyMaxHealth = (int)onwer.MaxHp.GetValue();
        if(enemyCurrentHealth < 0)
        {
            OnDied();
        }
    }

    public void ArmyCastleSetHealth(CastleSO onwer)
    {
        _playercastleStat = onwer;
        armyCastleCurrentHealth = armyCastleMaxHealth = (int)onwer.MaxHp.GetValue();
    }

    public void EnemyCastleSetHealth(EnemyCastleSo owner)
    {
        _enemyCastleStat = owner;
        enemyCastleCurrentHealth = enemyCastleMaxHealth = (int)owner.MaxHp.GetValue();
    }

    public void ArmyApplyDamage(int damage)
    {
        playerCurrentHealth -= damage;
    }

    public void EnemyApplyDamage(int damage)
    {
        enemyCurrentHealth -= damage;
    }

    public void OnDied()
    {
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        _anim.speed = 1f;

        _anim.SetBool(HASH_DEAD, true);
        _agent.enabled = false;
        _collider.enabled = false;
        _owner.IsDead = true;
        _owner.enabled = false;
    }

    public void ArmyCastleApplyDamage(int damage)
    {
        armyCastleCurrentHealth -= damage;
    }

    public void EnemyCastleApplyDamage(int damage)
    {
        enemyCastleCurrentHealth -= damage;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
