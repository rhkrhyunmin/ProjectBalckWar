using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastleControl : RangedAttack
{
    public float detectionRadius = 10f; // 탐지 반경
    private float timer = 0.0f;

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
            HandleEnemyEncounter(enemy);
        }
    }

    private void HandleEnemyEncounter(Collider2D enemy)
    {
        timer += Time.deltaTime; // timer에 경과 시간을 누적

        if (timer > 0.5f)
        {
            StartShooting(transform, PoolManager.Instance.Pop(PoolType.Arrow, transform.position), enemy.transform, 5, 5);
            timer = 0;
        }
    }

    public void UpRotation()
    {
        // 현재 각도를 eulerAngles로 변환
        Vector3 currentRotation = transform.eulerAngles;

        // Z축의 각도가 0에서 80 사이일 때만 증가
        if (currentRotation.z > -40 || currentRotation.z < 0)
        {
            currentRotation.z -= 1f;  
        }

        transform.eulerAngles = currentRotation;
    }

    public void DownRotation()
    {
        // 현재 각도를 eulerAngles로 변환
        Vector3 currentRotation = transform.eulerAngles;

        // Z축의 각도가 0에서 80 사이일 때만 증가
        if (currentRotation.z > 40 || currentRotation.z < 0)
        {
            currentRotation.z += 1f;
        }

        transform.eulerAngles = currentRotation;
    }
}
