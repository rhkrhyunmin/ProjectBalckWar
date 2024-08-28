using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    // �߻�ü �߻縦 �����ϴ� �Լ�
    public void StartShooting(Transform firePoint, PoolableMono Obj, Transform target, float fireRate, float projectileSpeed)
    {
        StartCoroutine(ShootProjectile(firePoint, Obj, target, fireRate, projectileSpeed));
    }

    // �߻�ü �߻縦 ���ߴ� �Լ�
    public void StopShooting()
    {
        StopAllCoroutines();
    }

    // �߻�ü�� ���� �ð� �������� �߻��ϴ� �ڷ�ƾ
    IEnumerator ShootProjectile(Transform firePoint, PoolableMono Obj, Transform target, float fireRate, float projectileSpeed)
    {
        while (true)
        {
            Fire(firePoint, Obj, target, projectileSpeed);
            yield return new WaitForSeconds(fireRate);
        }
    }

    // �߻�ü�� �߻��ϴ� �Լ�
    void Fire(Transform firePoint, PoolableMono Obj, Transform target, float projectileSpeed)
    {
        // �߻�ü �ν��Ͻ� ����
        Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogWarning("Rigidbody2D not found on Obj.");
            return;
        }

        // �߻� �ӵ� ��� (������ �)
        Vector2 launchVelocity = CalculateLaunchVelocity(firePoint.position, target.position, projectileSpeed);

        // Rigidbody2D�� �ӵ� ����
        rb.velocity = launchVelocity;

        // �߻�ü�� ȸ���� �ʱ� �ӵ��� ���� ���� (ù �߻� ��)
        RotateTowardsTarget(Obj.transform, rb.velocity);

        // �߻�ü�� ȸ���� ��� ������Ʈ�ϴ� ������Ʈ �߰�
        if (Obj.TryGetComponent<ProjectileUpdater>(out ProjectileUpdater updater) == false)
        {
            updater = Obj.gameObject.AddComponent<ProjectileUpdater>();
        }

        // �߻�ü�� Rigidbody2D�� ������ ȸ�� ������Ʈ
        updater.Initialize(rb);
    }

    // ������ ��� �߻� �ӵ��� ����ϴ� �Լ�
    Vector2 CalculateLaunchVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        // ���� �� ���� �Ÿ� ���
        float displacementX = targetPosition.x - startPosition.x;
        float displacementY = targetPosition.y - startPosition.y;

        // �߷� ���ӵ� (2D ȯ�濡�� ���)
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // �߻� ���� (45���� �����ϰų�, ���ϴ� ������ ���� ����)
        float angle = 45 * Mathf.Deg2Rad;

        // x ���� �ӵ� ���
        float speedX = Mathf.Cos(angle) * projectileSpeed;

        // y ���� �ӵ� ���
        float speedY = Mathf.Sin(angle) * projectileSpeed;

        // �ʿ��� �ʱ� �ӵ� ���
        float timeToTarget = Mathf.Abs(displacementX / speedX);
        float velocityY = (displacementY + 0.5f * gravity * timeToTarget * timeToTarget) / timeToTarget;

        // ���� �ӵ� ����
        Vector2 launchVelocity = new Vector2(speedX, velocityY);

        return launchVelocity;
    }

    // �߻�ü�� ���ư��� �������� ȸ����Ű�� �Լ�
    void RotateTowardsTarget(Transform objTransform, Vector2 velocity)
    {
        // �߻�ü�� ���ư��� �������� ȸ�� (Transform.up�� �ӵ� ���Ϳ� ��ġ�ϵ��� ��)
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        // �߻�ü�� up ������ �ӵ� ���Ϳ� ��ġ��Ű���� ȸ�� ����
        objTransform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}

public class ProjectileUpdater : MonoBehaviour
{
    private Rigidbody2D rb;

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
}
