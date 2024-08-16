using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public float speed = 10f;                      // ����ü �ӵ�
    public float height = 1f;                      // ������ ����
    public Vector3 targetPosition;                 // ��ǥ ��ġ
    public bool isHoming = false;                  // ���� ����

    public GameObject targetObject;                // ��ǥ�� �� ������Ʈ (���� ����ü�� ���)

    private Vector3 startPosition;                 // ����ü�� ���� ��ġ
    private float timeToTarget;                    // ��ǥ���� �ɸ��� �ð�
    private float elapsedTime = 0f;                // ����� �ð�
    private bool isLaunched = false;               // �߻� ���� Ȯ��

    // �ʱ�ȭ �Լ�
    public void Initialize(Vector3 target, float projectileSpeed, float projectileHeight, GameObject targetObj = null, bool homing = false)
    {
        startPosition = transform.position;
        targetPosition = target;
        speed = projectileSpeed;
        height = projectileHeight;
        targetObject = targetObj;
        isHoming = homing;

        // ��ǥ���� �ɸ��� �ð� ���
        timeToTarget = Vector3.Distance(startPosition, targetPosition) / speed;
        elapsedTime = 0f;
        isLaunched = true; // �߻� ���·� ����
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Initialize(new Vector3(5,0,0), 5, 2);
        }

        if (isLaunched)
        {
            LaunchProjectile();
        }
    }

    // ����ü�� �߻� ����
    private void LaunchProjectile()
    {
        elapsedTime += Time.deltaTime;

        // ���� ���� ���
        float t = Mathf.Clamp01(elapsedTime / timeToTarget);

        // ���� ����ü�� ��� ��ǥ ��ġ ����
        if (isHoming && targetObject != null)
        {
            targetPosition = targetObject.transform.position;
        }

        // ������ ��� ���
        Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t);

        // ������ ȿ�� �߰�
        float heightAtT = Mathf.Sin(t * Mathf.PI) * height;
        currentPos.y += heightAtT;

        // ���� ��ġ�� �̵�
        transform.position = currentPos;

        // ��ǥ�� ������ ���
        if (t >= 1f)
        {
            isLaunched = false;
            OnProjectileHit();
        }
    }

    // ��ǥ�� �������� �� ȣ��Ǵ� �Լ�
    private void OnProjectileHit()
    {
        Destroy(gameObject); // ����ü ����
    }
}
