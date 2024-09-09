using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PoolableMono
{
    private Quaternion rotation;
    private Rigidbody2D rb;

    public Quaternion Rotation
    {
        get { return rotation; }
        set
        {
            // Y축 회전을 고정한 상태에서 X와 Z축 회전만 변경
            rotation = Quaternion.Euler(value.eulerAngles.x, rotation.eulerAngles.y, value.eulerAngles.z);

            // 오브젝트에 적용
            transform.rotation = rotation;
        }
    }

    void Start()
    {
        // 초기 회전값을 설정
        rotation = transform.rotation;
    }

    // Rigidbody2D 초기화
    public void Initialize(Rigidbody2D rigidbody2D)
    {
        rb = rigidbody2D;
    }

    void Update()
    {
        if (rb != null && rb.velocity != Vector2.zero)
        {
            RotateTowardsTarget(transform, rb.velocity);
        }
    }

    // 발사체가 나아가는 방향으로 회전시키는 함수
    void RotateTowardsTarget(Transform objTransform, Vector2 velocity)
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        objTransform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            PoolManager.Instance.Push(this);
        }
    }
}
