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

        // 처음 발사할 때 바로 타겟 방향으로 회전
        RotateTowardsTarget(rb.velocity);
    }

    void Update()
    {
        if (target != null)
        {
            // 지속적으로 타겟을 향해 회전
            RotateTowardsTarget(rb.velocity);
        }
    }

    void RotateTowardsTarget(Vector2 velocity)
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); // 각도 조정
    }
}
