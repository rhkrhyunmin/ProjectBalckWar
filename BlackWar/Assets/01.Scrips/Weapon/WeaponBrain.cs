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
    protected State currentState;

    // 미리 정의된 레이어 상수
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public PlayerController playerController;

    // Raycast 관련 변수
    private float meleeRayDistance = 1f; 
    private float rangedRayDistance = 5f;

    private void Update()
    {
        if (weaponType == WeaponType.Melee)
        {
            ShootMeleeRaycast();
        }

        else if (weaponType == WeaponType.Ranged)
        {
            ShootRangedRaycast();
        }
    }

    public void ShootMeleeRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, meleeRayDistance, enemyLayer);

        if (hit.collider != null)
        {
            //playerController.Attack(playerController.stat.AttackPower.GetValue());
            Debug.Log("123");
        }
    }

    public void ShootRangedRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rangedRayDistance, enemyLayer | obstacleLayer);

        if (hit.collider != null)
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (IsInLayerMask(hitLayer, enemyLayer))
            {
               // playerController.Attack(playerController.stat.AttackPower.GetValue());
                PoolManager.Instance.Push(this);
            }
            // 장애물을 맞췄을 경우
            else if (IsInLayerMask(hitLayer, obstacleLayer))
            {
                Debug.Log("Ranged weapon hit an obstacle: " + hit.collider.name);
                PoolManager.Instance.Push(this); 
            }
        }
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) != 0);
    }
}
