using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 0 = 원거리 적, 1 ~ 3 = 근거리 적
    [SerializeField] List<Enemy> enemies;
    [SerializeField] Transform spawnPoint; // 적이 생성될 위치

    public CastleSO TowerStat;

    private float currentHealth;

    void Start()
    {
        TowerStat = Instantiate(TowerStat);
        currentHealth = TowerStat.MaxHp.GetValue();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        Debug.Log(currentHealth);
        while (currentHealth > 0)
        {
            Debug.Log("생성 실행됨");
            // 적들을 한 번에 소환하지 않고 차례대로 소환
            yield return StartCoroutine(SpawnBasedOnTowerHealth());
            yield return new WaitForSeconds(2f); // 2초마다 소환 주기
        }
    }

    IEnumerator SpawnBasedOnTowerHealth()
    {
        // 타워의 체력을 기반으로 소환할 적의 밸런스 조정
        float healthPercentage = currentHealth / TowerStat.MaxHp.GetValue();

        int numEnemies = 4; // 기본 적 소환 수
        if (healthPercentage < 0.75f) numEnemies += 1;
        if (healthPercentage < 0.5f) numEnemies += 2;
        if (healthPercentage < 0.25f) numEnemies += 3;

        for (int i = 0; i < numEnemies; i++)
        {
            float randomValue = Random.Range(0f, 1f);

            if (healthPercentage > 0.75f)
            {
                // 체력이 75% 이상일 때: 약한 적 위주
                if (randomValue < 0.6f)
                    SpawnEnemy(enemies[1]);
                else
                    SpawnEnemy(enemies[0]);
            }
            else if (healthPercentage > 0.6f)
            {
                // 체력이 60% 이상일 때: 중간 강도 적
                if (randomValue < 0.5f)
                    SpawnEnemy(enemies[0]);
                else if (randomValue < 0.8f)
                    SpawnEnemy(enemies[1]);
                else
                    SpawnEnemy(enemies[2]);
            }
            else if (healthPercentage > 0.45f)
            {
                // 체력이 45% 이상일 때: 강한 강도 적 생성 시작
                if (randomValue < 0.5f)
                    SpawnEnemy(enemies[0]);
                else if (randomValue < 0.8f)
                    SpawnEnemy(enemies[2]);
                else
                    SpawnEnemy(enemies[3]);
            }
            else
            {
                // 체력이 45% 이하일 때: 강한 적 위주
                if (randomValue < 0.3f)
                    SpawnEnemy(enemies[2]);
                else
                    SpawnEnemy(enemies[3]);
            }

            // 적 하나를 소환하고 잠시 대기
            yield return new WaitForSeconds(0.5f); // 각 적 사이의 대기 시간 (0.5초)
        }
    }

    void SpawnEnemy(Enemy enemyPrefab)
    {
        PoolManager.Instance.Pop(enemyPrefab.poolType, spawnPoint.position);
    }
}