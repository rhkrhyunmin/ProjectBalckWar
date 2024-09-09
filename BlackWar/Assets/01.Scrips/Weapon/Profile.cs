using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;

    public void Initialize(Vector2 target, float projectileSpeed)
    {
        this.target.position = target;
        rb = GetComponent<Rigidbody2D>();

        // ó�� �߻��� �� �ٷ� Ÿ�� �������� ȸ��
        RotateTowardsTarget(rb.velocity);
    }

    void Update()
    {
        if (target != null)
        {
            // ���������� Ÿ���� ���� ȸ��
            RotateTowardsTarget(rb.velocity);
        }
    }

    void RotateTowardsTarget(Vector2 velocity)
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); // ���� ����
    }
}
