using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint; // ���� ������ ��ġ
    public CastleSO TowerStat;

    private float currentHealth;
    private bool isSpawning = false; // �ߺ� ���� ����

    void Start()
    {
        currentHealth = TowerStat.MaxHp.GetValue();
        InvokeRepeating("StartSpawning", 0f, 10f); // 10�� �������� ��ȯ �ֱ� ����
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
        isSpawning = true; // �ڷ�ƾ�� ���� ������ ǥ��
        Debug.Log(currentHealth);

        while (currentHealth > 0)
        {
            Debug.Log("�� ���� �����");
            yield return StartCoroutine(SpawnBasedOnTowerHealth());
            yield return new WaitForSeconds(10); // �� �� ��ȯ �� 10�� ���
        }

        isSpawning = false; // �ڷ�ƾ �Ϸ� �� �ٽ� ��ȯ �����ϰ� ����
    }

    IEnumerator SpawnBasedOnTowerHealth()
    {
        float healthPercentage = currentHealth / TowerStat.MaxHp.GetValue();
        int numEnemies = CalculateNumEnemies(healthPercentage);

        for (int i = 0; i < numEnemies; i++)
        {
            PoolType enemyType = GetEnemyTypeBasedOnHealth(healthPercentage);
            SpawnEnemy(enemyType);
            yield return new WaitForSeconds(3); // �� �� ������ ��� �ð�
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
