using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCastleControl : MonoBehaviour
{
    [SerializeField] Transform spawnPoint; // 적이 생성될 위치
    public EnemyCastleSo TowerStat;

    public SlideFollow SlideFollow;

    public Health HealthCompo { get; private set; }

    private bool isSpawning = false; // 중복 실행 방지

    private void Awake()
    {
        HealthCompo = GetComponent<Health>(); // HealthCompo에 Health 컴포넌트 할당
        HealthCompo?.EnemyCastleSetHealth(TowerStat);
    }

    void Start()
    {
        StartCoroutine(SpawnEnemiesPeriodically()); // 코루틴으로 적 주기적으로 소환
    }

    private void Update()
    {
        SlideFollow.slider.value = HealthCompo.enemyCastleCurrentHealth;
    }

    IEnumerator SpawnEnemiesPeriodically()
    {
        while (HealthCompo.enemyCastleCurrentHealth > 0)
        {
            if (!isSpawning)
            {
                StartCoroutine(SpawnEnemies());
            }
            yield return new WaitForSeconds(10f); // 10초 대기 후 다시 소환 시도
        }
    }

    IEnumerator SpawnEnemies()
    {
        isSpawning = true; // 코루틴이 실행 중임을 표시
        Debug.Log(HealthCompo.enemyCastleCurrentHealth);

        while (HealthCompo.enemyCastleCurrentHealth > 0)
        {
            yield return StartCoroutine(SpawnBasedOnTowerHealth());
            yield return new WaitForSeconds(10); // 한 번 소환 후 10초 대기
        }

        isSpawning = false; // 코루틴 완료 후 다시 소환 가능하게 설정
    }

    IEnumerator SpawnBasedOnTowerHealth()
    {
        float healthPercentage = HealthCompo.enemyCastleCurrentHealth / TowerStat.MaxHp.GetValue();
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
            return (randomValue < 0.6f) ? PoolType.BigZombie : PoolType.BigZombie;
        }
        else if (healthPercentage > 0.6f)
        {
            return (randomValue < 0.5f) ? PoolType.BigZombie : PoolType.BigZombie;
        }
        else if (healthPercentage > 0.45f)
        {
            return (randomValue < 0.5f) ? PoolType.BigZombie : PoolType.BigZombie;
        }
        else
        {
            return (randomValue < 0.3f) ? PoolType.BigZombie : PoolType.BigStrongZombie;
        }
    }

    void SpawnEnemy(PoolType enemyType)
    {
        if (PoolManager.Instance != null)
        {
            PoolManager.Instance.Pop(enemyType, spawnPoint.transform.position);
        }
    }
}
