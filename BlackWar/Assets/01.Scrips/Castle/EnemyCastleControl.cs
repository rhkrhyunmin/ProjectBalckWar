using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCastleControl : MonoBehaviour
{
    [SerializeField] Transform spawnPoint; // ���� ������ ��ġ
    public EnemyCastleSo TowerStat;

    public SlideFollow SlideFollow;

    public Health HealthCompo { get; private set; }

    private bool isSpawning = false; // �ߺ� ���� ����

    private void Awake()
    {
        HealthCompo = GetComponent<Health>(); // HealthCompo�� Health ������Ʈ �Ҵ�
        HealthCompo?.EnemyCastleSetHealth(TowerStat);
    }

    void Start()
    {
        StartCoroutine(SpawnEnemiesPeriodically()); // �ڷ�ƾ���� �� �ֱ������� ��ȯ
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
            yield return new WaitForSeconds(10f); // 10�� ��� �� �ٽ� ��ȯ �õ�
        }
    }

    IEnumerator SpawnEnemies()
    {
        isSpawning = true; // �ڷ�ƾ�� ���� ������ ǥ��
        Debug.Log(HealthCompo.enemyCastleCurrentHealth);

        while (HealthCompo.enemyCastleCurrentHealth > 0)
        {
            yield return StartCoroutine(SpawnBasedOnTowerHealth());
            yield return new WaitForSeconds(10); // �� �� ��ȯ �� 10�� ���
        }

        isSpawning = false; // �ڷ�ƾ �Ϸ� �� �ٽ� ��ȯ �����ϰ� ����
    }

    IEnumerator SpawnBasedOnTowerHealth()
    {
        float healthPercentage = HealthCompo.enemyCastleCurrentHealth / TowerStat.MaxHp.GetValue();
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
