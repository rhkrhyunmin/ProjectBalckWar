using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 무기 타입 열거형 정의
public enum WeaponType
{
    Melee,
    Ranged
}

public class WeaponBrain : PoolableMono
{
    // 무기 타입 설정
    public WeaponType weaponType;

    // 미리 정의된 레이어 상수
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    // Raycast 관련 변수
    public float rayDistance = 10f; // Raycast 사정 거리

    private void Update()
    {
        // 원거리 무기일 경우 Raycast를 계속해서 쏩니다.
        if (weaponType == WeaponType.Ranged)
        {
            ShootRaycast();
        }
    }

    // 근접 무기 충돌 처리 함수 (Collider 사용)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weaponType == WeaponType.Melee)
        {
            int otherLayer = collision.gameObject.layer;

            if (IsInLayerMask(otherLayer, enemyLayer))
            {
                HandleMeleeCollision(collision);
            }
        }
    }

    // 근접 무기 충돌 처리 함수
    private void HandleMeleeCollision(Collider2D other)
    {
        Debug.Log("Melee weapon hit an enemy: " + other.name);
        // 적에게 데미지를 입히는 로직 추가
        // DamageEnemy(other.gameObject);
    }

    // 원거리 무기 Raycast 발사 함수
    private void ShootRaycast()
    {
        // 무기의 위치에서 Raycast 발사
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDistance, enemyLayer | obstacleLayer);

        if (hit.collider != null)
        {
            int hitLayer = hit.collider.gameObject.layer;

            // 적을 맞췄을 경우
            if (IsInLayerMask(hitLayer, enemyLayer))
            {
                Debug.Log("Ranged weapon hit an enemy: " + hit.collider.name);
                // 적에게 데미지를 입히는 로직 추가
                // DamageEnemy(hit.collider.gameObject);
                PoolManager.Instance.Push(this); // 발사체 회수
            }
            // 장애물을 맞췄을 경우
            else if (IsInLayerMask(hitLayer, obstacleLayer))
            {
                Debug.Log("Ranged weapon hit an obstacle: " + hit.collider.name);
                PoolManager.Instance.Push(this); // 발사체 회수
            }
        }
    }

    // 특정 레이어가 지정된 LayerMask에 속하는지 확인하는 함수
    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) != 0);
    }
}
