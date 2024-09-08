using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    // ������ ���� ����Ʈ
    private List<Enemy> enemies = new List<Enemy>();

    // ���� ����Ʈ�� �߰��ϴ� �޼���
    public void AddEnemy(Enemy enemy)
    {
        if (enemy != null && !enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    // ���� ����Ʈ���� �����ϴ� �޼���
    public void RemoveEnemy(Enemy enemy)
    {
        if (enemy != null && enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    // ��� ������ ó���ϴ� �޼��� (��: ��� ���鿡�� �������� �� ��)
    /*public void DamageAllEnemies(float damage)
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.OnHit(damage);
            }
        }
    }*/

    // ���� ���� ����Ʈ�� �������� �޼���
    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
}
