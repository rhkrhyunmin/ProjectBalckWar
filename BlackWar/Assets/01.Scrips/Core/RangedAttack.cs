using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    // �߻�ü�� ���� �ð� �������� �߻��ϴ� �ڷ�ƾ (������ �߻�)
    public IEnumerator ShootProjectile(Transform firePoint, PoolableMono obj, Transform target, float fireRate, float projectileSpeed, bool isParabolic)
    {
        while (true)
        {
            Fire(firePoint.position, target.position, obj, projectileSpeed, isParabolic); // isParabolic �� false�� ����
            yield return new WaitForSeconds(fireRate);
        }
    }


    void Fire(Vector3 startPosition, Vector3 targetPosition, PoolableMono obj, float projectileSpeed, bool isParabolic = false)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        Vector2 launchVelocity;

        // ������ ��� ������� ���ο� ���� �߻� �ӵ��� ���
        if (isParabolic)
        {
            launchVelocity = CalculateParabolicVelocity(startPosition, targetPosition, projectileSpeed);
        }
        else
        {
            launchVelocity = CalculateStraightlineVelocity(startPosition, targetPosition, projectileSpeed);
        }

        rb.velocity = launchVelocity;
        RotateTowardsTarget(obj.transform, rb.velocity);
    }


    // ������ ��� �߻� �ӵ��� ����ϴ� �Լ�
    Vector2 CalculateParabolicVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        float displacementX = targetPosition.x - startPosition.x;
        float displacementY = targetPosition.y - startPosition.y;
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        float angle = 45 * Mathf.Deg2Rad;  // 45�� ������ �߻�

        float speedX = Mathf.Cos(angle) * projectileSpeed;
        float timeToTarget = Mathf.Abs(displacementX / speedX);

        float velocityY = (displacementY + 0.5f * gravity * timeToTarget * timeToTarget) / timeToTarget;
        Vector2 launchVelocity = new Vector2(speedX, velocityY);

        return launchVelocity;
    }


    // �������� �߻��� ���� �߻� �ӵ��� ����ϴ� �Լ�
    Vector2 CalculateStraightlineVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        // ��ǥ ��ġ���� ���� ���
        Vector2 direction = (targetPosition - startPosition).normalized;

        // ���� ���Ϳ� �߻� �ӵ��� ���Ͽ� �߻� �ӵ� ���� ����
        Vector2 launchVelocity = direction * projectileSpeed;

        return launchVelocity;
    }

    // �߻�ü�� ���ư��� �������� ȸ����Ű�� �Լ�
    void RotateTowardsTarget(Transform objTransform, Vector2 velocity)
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        objTransform.rotation = Quaternion.Euler(0, 0, angle); // ���� ����
    }
}