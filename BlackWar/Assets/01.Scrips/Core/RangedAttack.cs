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

    void Fire(Transform firePoint, PoolableMono Obj, Transform target, float projectileSpeed)
    {
        Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();

        Vector2 launchVelocity = CalculateLaunchVelocity(firePoint.position, target.position, projectileSpeed);

        float initialAngle = firePoint.eulerAngles.x;
        launchVelocity = RotateVectorByAngle(launchVelocity, initialAngle);

        rb.velocity = launchVelocity;

        RotateTowardsTarget(Obj.transform, rb.velocity);

        if (Obj.TryGetComponent<ProjectileUpdater>(out ProjectileUpdater updater) == false)
        {
            updater = Obj.gameObject.AddComponent<ProjectileUpdater>();
        }

        updater.Initialize(rb);
    }

    Vector2 RotateVectorByAngle(Vector2 vector, float angle)
    {
        // Quaternion�� ����Ͽ� ȸ�� ��� ����
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector2 rotatedVector = rotation * vector;

        return rotatedVector;
    }

    // ������ ��� �߻� �ӵ��� ����ϴ� �Լ�
    Vector2 CalculateLaunchVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        float displacementX = targetPosition.x - startPosition.x;
        float displacementY = targetPosition.y - startPosition.y;

        float gravity = Mathf.Abs(Physics2D.gravity.y);

        float angle =  Mathf.Deg2Rad;

        float speedX = Mathf.Cos(angle) * projectileSpeed;

        float speedY = Mathf.Sin(angle) * projectileSpeed;

        float timeToTarget = Mathf.Abs(displacementX / speedX);
        float velocityY = (displacementY + 0.5f * gravity * timeToTarget * timeToTarget) / timeToTarget;

        Vector2 launchVelocity = new Vector2(speedX, velocityY);

        return launchVelocity;
    }

    void RotateTowardsTarget(Transform objTransform, Vector2 velocity)
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        objTransform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

}

public class ProjectileUpdater : PoolableMono
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            PoolManager.Instance.Push(this);
        }
    }
}
