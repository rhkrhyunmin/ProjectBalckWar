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

    // �̸� ���ǵ� ���̾� ���
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    // Raycast ���� ����
    public float rayDistance = 10f; // Raycast ���� �Ÿ�

    private void Update()
    {
        // ���Ÿ� ������ ��� Raycast�� ����ؼ� ���ϴ�.
        if (weaponType == WeaponType.Ranged)
        {
            ShootRaycast();
        }
    }

    // ���� ���� �浹 ó�� �Լ� (Collider ���)
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

    // ���� ���� �浹 ó�� �Լ�
    private void HandleMeleeCollision(Collider2D other)
    {
        Debug.Log("Melee weapon hit an enemy: " + other.name);
        // ������ �������� ������ ���� �߰�
        // DamageEnemy(other.gameObject);
    }

    // ���Ÿ� ���� Raycast �߻� �Լ�
    private void ShootRaycast()
    {
        // ������ ��ġ���� Raycast �߻�
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDistance, enemyLayer | obstacleLayer);

        if (hit.collider != null)
        {
            int hitLayer = hit.collider.gameObject.layer;

            // ���� ������ ���
            if (IsInLayerMask(hitLayer, enemyLayer))
            {
                Debug.Log("Ranged weapon hit an enemy: " + hit.collider.name);
                // ������ �������� ������ ���� �߰�
                // DamageEnemy(hit.collider.gameObject);
                PoolManager.Instance.Push(this); // �߻�ü ȸ��
            }
            // ��ֹ��� ������ ���
            else if (IsInLayerMask(hitLayer, obstacleLayer))
            {
                Debug.Log("Ranged weapon hit an obstacle: " + hit.collider.name);
                PoolManager.Instance.Push(this); // �߻�ü ȸ��
            }
        }
    }

    // Ư�� ���̾ ������ LayerMask�� ���ϴ��� Ȯ���ϴ� �Լ�
    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) != 0);
    }
}
