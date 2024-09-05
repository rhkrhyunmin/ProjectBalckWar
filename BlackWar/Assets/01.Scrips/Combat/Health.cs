using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int playerMaxHealth;
    public int enemyMaxHealth;
    public int playerCurrentHealth;
    public int enemyCurrentHealth;


    private PlayerStat _playerStat;
    private EnemyStat _enemyStat;
    
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
    }

    public void EnemyApplyDamage(int damage)
    {
        enemyCurrentHealth -= damage;
    }
}
