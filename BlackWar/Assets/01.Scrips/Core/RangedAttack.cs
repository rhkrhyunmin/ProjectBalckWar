using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public void StartShooting(Transform firePoint, PoolableMono Obj, Transform target, float fireRate, float projectileSpeed)
    {
        StartCoroutine(ShootProjectile(firePoint, Obj, target, fireRate, projectileSpeed));
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }

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

        // �߻� �ӵ� ��� (������ �)
        Vector2 launchVelocity = CalculateLaunchVelocity(firePoint.position, target.position, projectileSpeed);

        // Rigidbody2D�� �ӵ� ����
        rb.velocity = launchVelocity;

        // �߻�ü�� ��ǥ�� ���� ȸ���ϵ��� ����
        RotateTowardsTarget(Obj.transform, rb.velocity);
    }

    Vector2 CalculateLaunchVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        // ���� �� ���� �Ÿ� ���
        float displacementX = targetPosition.x - startPosition.x;
        float displacementY = targetPosition.y - startPosition.y;

        // �߷� ���ӵ� (2D ȯ�濡�� ���)
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // �߻� ���� (45���� �����ϰų�, ���ϴ� ������ ���� ����)
        float angle = 45f * Mathf.Deg2Rad;

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

    void RotateTowardsTarget(Transform objTransform, Vector2 velocity)
    {
        // �ӵ� ���� ������ ������ ��ȯ
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        // �߻�ü�� ȸ���� �ӵ� ���� �������� ����
        objTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
