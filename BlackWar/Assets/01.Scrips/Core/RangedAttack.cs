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
        // PoolManager�� ���� �߻�ü �������� (�ʿ� �� ����)
        Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();

        // �߻� �ӵ� ��� (������ �)
        Vector2 launchVelocity = CalculateLaunchVelocity(firePoint.position, target.position, projectileSpeed);

        // Rigidbody2D�� �ӵ� ����
        rb.velocity = launchVelocity;

        // �߻�ü�� ��ǥ�� ���� ȸ���ϵ��� ����
        RotateTowardsTarget(Obj.transform, target.position);
    }

    Vector2 CalculateLaunchVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        Vector2 direction = targetPosition - startPosition;

        // ��ǥ������ �Ÿ�
        float distance = direction.magnitude;

        // �߻� ���� (45���� ����)
        float angle = 45f * Mathf.Deg2Rad;

        // �߷� �� (Physics2D�� �߷� ���)
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // ������ � �ӵ� ��� ����
        float vSquared = (gravity * distance * distance) / (2 * Mathf.Cos(angle) * Mathf.Cos(angle) * (distance * Mathf.Tan(angle) - direction.y));

        // �ӵ��� sqrt�� ����
        float velocity = Mathf.Sqrt(vSquared);

        // �ӵ� ���� ���ϱ�
        Vector2 launchVelocity = new Vector2(Mathf.Cos(angle) * velocity, Mathf.Sin(angle) * velocity);

        // Ÿ�� ��ġ�� �°� �ӵ� ������ ����
        return launchVelocity.normalized * projectileSpeed;
    }

    void RotateTowardsTarget(Transform objTransform, Vector3 targetPosition)
    {
        // ���� ��ġ�� ��ǥ ��ġ ������ ���� ���� ���
        Vector3 direction = (targetPosition - objTransform.position).normalized;

        // �߻�ü�� ȸ���� ��ǥ �������� ����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        objTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
