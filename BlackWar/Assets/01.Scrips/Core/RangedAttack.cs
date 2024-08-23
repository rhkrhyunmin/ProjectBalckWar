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
        // PoolManager를 통해 발사체 가져오기 (필요 시 구현)
        Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();

        // 발사 속도 계산 (포물선 운동)
        Vector2 launchVelocity = CalculateLaunchVelocity(firePoint.position, target.position, projectileSpeed);

        // Rigidbody2D의 속도 설정
        rb.velocity = launchVelocity;

        // 발사체가 목표를 향해 회전하도록 설정
        RotateTowardsTarget(Obj.transform, target.position);
    }

    Vector2 CalculateLaunchVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        Vector2 direction = targetPosition - startPosition;

        // 목표까지의 거리
        float distance = direction.magnitude;

        // 발사 각도 (45도로 고정)
        float angle = 45f * Mathf.Deg2Rad;

        // 중력 값 (Physics2D의 중력 사용)
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // 포물선 운동 속도 계산 공식
        float vSquared = (gravity * distance * distance) / (2 * Mathf.Cos(angle) * Mathf.Cos(angle) * (distance * Mathf.Tan(angle) - direction.y));

        // 속도는 sqrt로 구함
        float velocity = Mathf.Sqrt(vSquared);

        // 속도 성분 구하기
        Vector2 launchVelocity = new Vector2(Mathf.Cos(angle) * velocity, Mathf.Sin(angle) * velocity);

        // 타겟 위치에 맞게 속도 방향을 조정
        return launchVelocity.normalized * projectileSpeed;
    }

    void RotateTowardsTarget(Transform objTransform, Vector3 targetPosition)
    {
        // 현재 위치와 목표 위치 사이의 방향 벡터 계산
        Vector3 direction = (targetPosition - objTransform.position).normalized;

        // 발사체의 회전을 목표 방향으로 설정
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        objTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
