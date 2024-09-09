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
        // 캐슬 오브젝트의 위치를 기준으로 탐지 반경 내의 모든 적을 검사합니다.
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, castleStat.angle.GetValue(), enemyLayer);

        foreach (Collider2D enemy in enemiesInRange)
        {
            HandleEnemyEncounter(enemy);
        }
    }

    private void HandleEnemyEncounter(Collider2D enemy)
    {
        timer += Time.deltaTime; // timer에 경과 시간을 누적

        if (timer > 0.5f)
        {
            StartCoroutine(ShootProjectile(BowSpawn.transform,PoolManager.Instance.Pop(PoolType.Arrow,BowSpawn.transform.position), enemy.transform,2,10, false));
            timer = 0;
        }
    }

    public void UpRotation()
    {
        // 현재 각도를 eulerAngles로 변환
        Vector3 currentRotation = BowSpawn.transform.eulerAngles;

        // Z축의 각도가 0에서 80 사이일 때만 증가
        if (currentRotation.z > -40 || currentRotation.z < 0)
        {
            currentRotation.z -= 1f;  
        }

        BowSpawn.transform.eulerAngles = currentRotation;
    }

    public void DownRotation()
    {
        // 현재 각도를 eulerAngles로 변환
        Vector3 currentRotation = BowSpawn.transform.eulerAngles;

        // Z축의 각도가 0에서 80 사이일 때만 증가
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
