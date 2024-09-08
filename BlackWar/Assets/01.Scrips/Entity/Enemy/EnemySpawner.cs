using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint; // 적이 생성될 위치
    public CastleSO TowerStat;

    private float currentHealth;
    private bool isSpawning = false; // 중복 실행 방지

    void Start()
    {
        currentHealth = TowerStat.MaxHp.GetValue();
        InvokeRepeating("StartSpawning", 0f, 10f); // 10초 간격으로 소환 주기 설정
    }

    void StartSpawning()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        isSpawning = true; // 코루틴이 실행 중임을 표시
        Debug.Log(currentHealth);

        while (currentHealth > 0)
        {
            Debug.Log("적 생성 실행됨");
            yield return StartCoroutine(SpawnBasedOnTowerHealth());
            yield return new WaitForSeconds(10); // 한 번 소환 후 10초 대기
        }

        isSpawning = false; // 코루틴 완료 후 다시 소환 가능하게 설정
    }

    IEnumerator SpawnBasedOnTowerHealth()
    {
        float healthPercentage = currentHealth / TowerStat.MaxHp.GetValue();
        int numEnemies = CalculateNumEnemies(healthPercentage);

        for (int i = 0; i < numEnemies; i++)
        {
            PoolType enemyType = GetEnemyTypeBasedOnHealth(healthPercentage);
            SpawnEnemy(enemyType);
            yield return new WaitForSeconds(3); // 각 적 사이의 대기 시간
        }
    }

    int CalculateNumEnemies(float healthPercentage)
    {
        int numEnemies = 4;
        if (healthPercentage < 0.75f) numEnemies += 1;
        if (healthPercentage < 0.5f) numEnemies += 2;
        if (healthPercentage < 0.25f) numEnemies += 3;
        return numEnemies;
    }

    PoolType GetEnemyTypeBasedOnHealth(float healthPercentage)
    {
        float randomValue = Random.Range(0f, 1f);

        if (healthPercentage > 0.75f)
        {
            return (randomValue < 0.6f) ? PoolType.Hog : PoolType.CannonHog;
        }
        else if (healthPercentage > 0.6f)
        {
            return (randomValue < 0.5f) ? PoolType.CannonHog : PoolType.CannonHog;
        }
        else if (healthPercentage > 0.45f)
        {
            return (randomValue < 0.5f) ? PoolType.Hog : PoolType.CannonHog;
        }
        else
        {
            return (randomValue < 0.3f) ? PoolType.Hog : PoolType.CannonHog;
        }
    }

    void SpawnEnemy(PoolType enemyType)
    {
        if (PoolManager.Instance != null)
        {
            PoolManager.Instance.Pop(enemyType, spawnPoint.transform.position);
        }
        else
        {
            Debug.LogError("PoolManager.Instance is null. Cannot spawn enemies.");
        }
    }
}
