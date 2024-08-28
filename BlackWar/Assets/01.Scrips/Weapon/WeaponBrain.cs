using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� Ÿ�� ������ ����
public enum WeaponType
{
    Melee,
    Ranged
}

public class WeaponBrain : PoolableMono
{
    // ���� Ÿ�� ����
    public WeaponType weaponType;
    protected State currentState;

    // �̸� ���ǵ� ���̾� ���
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public PlayerController playerController;

    // Raycast ���� ����
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
            // ��ֹ��� ������ ���
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
