using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 0 = ���Ÿ� ��, 1 ~ 3 = �ٰŸ� ��
    [SerializeField] List<Enemy> enemies;
    [SerializeField] Transform spawnPoint; // ���� ������ ��ġ

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
            Debug.Log("���� �����");
            // ������ �� ���� ��ȯ���� �ʰ� ���ʴ�� ��ȯ
            yield return StartCoroutine(SpawnBasedOnTowerHealth());
            yield return new WaitForSeconds(2f); // 2�ʸ��� ��ȯ �ֱ�
        }
    }

    IEnumerator SpawnBasedOnTowerHealth()
    {
        // Ÿ���� ü���� ������� ��ȯ�� ���� �뷱�� ����
        float healthPercentage = currentHealth / TowerStat.MaxHp.GetValue();

        int numEnemies = 4; // �⺻ �� ��ȯ ��
        if (healthPercentage < 0.75f) numEnemies += 1;
        if (healthPercentage < 0.5f) numEnemies += 2;
        if (healthPercentage < 0.25f) numEnemies += 3;

        for (int i = 0; i < numEnemies; i++)
        {
            float randomValue = Random.Range(0f, 1f);

            if (healthPercentage > 0.75f)
            {
                // ü���� 75% �̻��� ��: ���� �� ����
                if (randomValue < 0.6f)
                    SpawnEnemy(enemies[1]);
                else
                    SpawnEnemy(enemies[0]);
            }
            else if (healthPercentage > 0.6f)
            {
                // ü���� 60% �̻��� ��: �߰� ���� ��
                if (randomValue < 0.5f)
                    SpawnEnemy(enemies[0]);
                else if (randomValue < 0.8f)
                    SpawnEnemy(enemies[1]);
                else
                    SpawnEnemy(enemies[2]);
            }
            else if (healthPercentage > 0.45f)
            {
                // ü���� 45% �̻��� ��: ���� ���� �� ���� ����
                if (randomValue < 0.5f)
                    SpawnEnemy(enemies[0]);
                else if (randomValue < 0.8f)
                    SpawnEnemy(enemies[2]);
                else
                    SpawnEnemy(enemies[3]);
            }
            else
            {
                // ü���� 45% ������ ��: ���� �� ����
                if (randomValue < 0.3f)
                    SpawnEnemy(enemies[2]);
                else
                    SpawnEnemy(enemies[3]);
            }

            // �� �ϳ��� ��ȯ�ϰ� ��� ���
            yield return new WaitForSeconds(0.5f); // �� �� ������ ��� �ð� (0.5��)
        }
    }

    void SpawnEnemy(Enemy enemyPrefab)
    {
        PoolManager.Instance.Pop(enemyPrefab.poolType, spawnPoint.position);
    }
}