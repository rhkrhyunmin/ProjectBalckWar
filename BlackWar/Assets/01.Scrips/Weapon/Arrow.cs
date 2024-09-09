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
            // Y�� ȸ���� ������ ���¿��� X�� Z�� ȸ���� ����
            rotation = Quaternion.Euler(value.eulerAngles.x, rotation.eulerAngles.y, value.eulerAngles.z);

            // ������Ʈ�� ����
            transform.rotation = rotation;
        }
    }

    void Start()
    {
        // �ʱ� ȸ������ ����
        rotation = transform.rotation;
    }

    // Rigidbody2D �ʱ�ȭ
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

    // �߻�ü�� ���ư��� �������� ȸ����Ű�� �Լ�
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
