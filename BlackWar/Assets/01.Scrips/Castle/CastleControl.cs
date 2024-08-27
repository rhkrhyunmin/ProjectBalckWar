using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastleControl : RangedAttack
{
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

    public void UpRotation()
    {
        // ���� ������ eulerAngles�� ��ȯ
        Vector3 currentRotation = transform.eulerAngles;

        // Z���� ������ 0���� 80 ������ ���� ����
        if (currentRotation.z > -40 || currentRotation.z < 0)
        {
            currentRotation.z -= 1f;  
        }

        transform.eulerAngles = currentRotation;
    }

    public void DownRotation()
    {
        // ���� ������ eulerAngles�� ��ȯ
        Vector3 currentRotation = transform.eulerAngles;

        // Z���� ������ 0���� 80 ������ ���� ����
        if (currentRotation.z > 40 || currentRotation.z < 0)
        {
            currentRotation.z += 1f;
        }

        transform.eulerAngles = currentRotation;
    }
}
