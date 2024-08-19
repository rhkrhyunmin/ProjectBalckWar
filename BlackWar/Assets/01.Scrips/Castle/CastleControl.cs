using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastleControl : RangedAttack
{
    public LayerMask enemyLayer; // �� ���̾�
    public float detectionRadius = 10f; // Ž�� �ݰ�
    private float timer = 0.0f;

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
        timer += Time.deltaTime; // timer�� ��� �ð��� ����

        if (timer > 0.5f)
        {
            StartShooting(transform, PoolManager.Instance.Pop(PoolType.Arrow, transform.position), enemy.transform, 5, 5);
            timer = 0;
        }
    }
}
