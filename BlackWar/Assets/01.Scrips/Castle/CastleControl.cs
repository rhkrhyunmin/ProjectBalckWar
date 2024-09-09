using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastleControl : RangedAttack
{
    public GameObject BowSpawn;


    public CastleSO castleStat;
    public LayerMask enemyLayer;
    public SlideFollow slideFollow;

    public Health HealthCompo { get; private set; }

    private float timer = 0.0f;

    private void Awake()
    {
        HealthCompo = GetComponent<Health>();
        HealthCompo?.ArmyCastleSetHealth(castleStat);
    }

    private void Update()
    {
        CheckForEnemies();
        slideFollow.slider.value = HealthCompo.armyCastleCurrentHealth;
    }

    private void CheckForEnemies()
    {
        // ĳ�� ������Ʈ�� ��ġ�� �������� Ž�� �ݰ� ���� ��� ���� �˻��մϴ�.
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, castleStat.angle.GetValue(), enemyLayer);

        foreach (Collider2D enemy in enemiesInRange)
        {
            HandleEnemyEncounter(enemy);
        }
    }

    private void HandleEnemyEncounter(Collider2D enemy)
    {
        timer += Time.deltaTime; // timer�� ��� �ð��� ����

        if (timer > 0.5f)
        {
            StartCoroutine(ShootProjectile(BowSpawn.transform,PoolManager.Instance.Pop(PoolType.Arrow,BowSpawn.transform.position), enemy.transform,2,10, false));
            timer = 0;
        }
    }

    public void UpRotation()
    {
        // ���� ������ eulerAngles�� ��ȯ
        Vector3 currentRotation = BowSpawn.transform.eulerAngles;

        // Z���� ������ 0���� 80 ������ ���� ����
        if (currentRotation.z > -40 || currentRotation.z < 0)
        {
            currentRotation.z -= 1f;  
        }

        BowSpawn.transform.eulerAngles = currentRotation;
    }

    public void DownRotation()
    {
        // ���� ������ eulerAngles�� ��ȯ
        Vector3 currentRotation = BowSpawn.transform.eulerAngles;

        // Z���� ������ 0���� 80 ������ ���� ����
        if (currentRotation.z > 40 || currentRotation.z < 0)
        {
            currentRotation.z += 1f;
        }

        BowSpawn.transform.eulerAngles = currentRotation;
    }

    /*public void OnHit(float damage)
    {
        currentHp -= damage;
    }*/
}
