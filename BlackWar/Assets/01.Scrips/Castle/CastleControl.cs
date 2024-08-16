using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleControl : RangedAttack
{
    public LayerMask enemyLayer; // �� ���̾�
    public float detectionRadius = 10f; // Ž�� �ݰ�

    private void Update()
    {
        CheckForEnemies();
    }

    private void CheckForEnemies()
    {
        // ĳ�� ������Ʈ�� ��ġ�� �������� Ž�� �ݰ� ���� ��� ���� �˻��մϴ�.
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
