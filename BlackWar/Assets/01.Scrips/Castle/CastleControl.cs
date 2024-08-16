using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleControl : RangedAttack
{
    public LayerMask enemyLayer; // 적 레이어
    public float detectionRadius = 10f; // 탐지 반경

    private void Update()
    {
        CheckForEnemies();
    }

    private void CheckForEnemies()
    {
        // 캐슬 오브젝트의 위치를 기준으로 탐지 반경 내의 모든 적을 검사합니다.
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

        foreach (Collider2D enemy in enemiesInRange)
        {
            Debug.Log("Enemy detected");
            HandleEnemyEncounter(enemy);
        }
    }

    private void HandleEnemyEncounter(Collider2D enemy)
    {
        Debug.Log("Handling enemy encounter");
        LaunchProjectile(enemy.transform.position, 10, 2, PoolManager.Instance.Pop(PoolType.Arrow,transform.position), false, false);
    }
}
