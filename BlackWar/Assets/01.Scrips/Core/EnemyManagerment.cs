using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    // 적들을 담을 리스트
    private List<Enemy> enemies = new List<Enemy>();

    // 적을 리스트에 추가하는 메서드
    public void AddEnemy(Enemy enemy)
    {
        if (enemy != null && !enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    // 적을 리스트에서 제거하는 메서드
    public void RemoveEnemy(Enemy enemy)
    {
        if (enemy != null && enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    // 모든 적들을 처리하는 메서드 (예: 모든 적들에게 데미지를 줄 때)
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

    // 현재 적의 리스트를 가져오는 메서드
    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
}
