using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
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
    }

    public void EnemyApplyDamage(int damage)
    {
        enemyCurrentHealth -= damage;
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
