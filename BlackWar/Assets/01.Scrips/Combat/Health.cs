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

    protected Animator _anim, _enemyAnim;
    protected Collider2D _collider;

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
        _enemyOwner = GetComponent<Enemy>();
        _collider = GetComponent<Collider2D>();
        if(_owner != null )
        _anim = transform.Find("Visual").GetComponent<Animator>();
        if(_enemyOwner != null )
        _enemyAnim = transform.Find("Visual").GetComponent<Animator>();
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
        
        if(playerCurrentHealth <= 0)
        {
            OnArmyDied();
        }
    }

    public void EnemyApplyDamage(int damage)
    {
        enemyCurrentHealth -= damage;

        if (enemyCurrentHealth <= 0)
        {
            OnEnemyDied();
        }
    }

    public void OnArmyDied()
    {
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        _anim.speed = 1f;

        _anim.SetBool(HASH_DEAD, true);
        //_collider.enabled = false;
        _owner.IsDead = true;
        //_owner.enabled = false;
    }

    public void OnEnemyDied()
    {
        var parameters = _enemyAnim.parameters;
        foreach (var param in parameters)
            _enemyAnim.SetBool(param.name, false);

        _enemyAnim.speed = 1f;

        _enemyAnim.SetBool(HASH_DEAD, true);
        //_collider.enabled = false;
        _enemyOwner.IsDead = true;
        //_enemyOwner.enabled = false;
    }

    public void ArmyCastleApplyDamage(int damage)
    {
        armyCastleCurrentHealth -= damage;
    }

    public void EnemyCastleApplyDamage(int damage)
    {
        enemyCastleCurrentHealth -= damage;
    }
}
