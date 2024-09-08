using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //0 = 원거리 적, 1 ~ 3 = 근거리 적
    [SerializeField] List<GameObject> enemies;
    [SerializeField] Transform spawnPoint; // 적이 생성될 위치들
    
    public CastleSO TowerStat;

    private float currentHealth;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        TowerStat = Instantiate(TowerStat);
        currentHealth = TowerStat.MaxHp.GetValue();
    }

    IEnumerator SpawnEnemies()
    {
        while (currentHealth > 0)
        {
            SpawnBasedOnTowerHealth();
            yield return new WaitForSeconds(5f); // 5초마다 적을 소환
        }
    }

    void SpawnBasedOnTowerHealth()
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
                    SpawnEnemy(enemies[0]);
                else
                    SpawnEnemy(enemies[1]);
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
                // 체력이 50% 이하일 때: 강한 적 위주
                if (randomValue < 0.3f)
                    SpawnEnemy(enemies[2]);
                else
                    SpawnEnemy(enemies[3]);
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}