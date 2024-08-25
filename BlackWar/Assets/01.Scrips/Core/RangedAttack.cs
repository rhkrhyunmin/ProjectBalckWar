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

        // 발사 속도 계산 (포물선 운동)
        Vector2 launchVelocity = CalculateLaunchVelocity(firePoint.position, target.position, projectileSpeed);

        // Rigidbody2D의 속도 설정
        rb.velocity = launchVelocity;

        // 발사체가 목표를 향해 회전하도록 설정
        RotateTowardsTarget(Obj.transform, rb.velocity);
    }

    Vector2 CalculateLaunchVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        // 수평 및 수직 거리 계산
        float displacementX = targetPosition.x - startPosition.x;
        float displacementY = targetPosition.y - startPosition.y;

        // 중력 가속도 (2D 환경에서 사용)
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // 발사 각도 (45도를 가정하거나, 원하는 각도로 설정 가능)
        float angle = 45f * Mathf.Deg2Rad;

        // x 방향 속도 계산
        float speedX = Mathf.Cos(angle) * projectileSpeed;

        // y 방향 속도 계산
        float speedY = Mathf.Sin(angle) * projectileSpeed;

        // 필요한 초기 속도 계산
        float timeToTarget = Mathf.Abs(displacementX / speedX);
        float velocityY = (displacementY + 0.5f * gravity * timeToTarget * timeToTarget) / timeToTarget;

        // 최종 속도 벡터
        Vector2 launchVelocity = new Vector2(speedX, velocityY);

        return launchVelocity;
    }

    void RotateTowardsTarget(Transform objTransform, Vector2 velocity)
    {
        // 속도 벡터 방향을 각도로 변환
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        // 발사체의 회전을 속도 벡터 방향으로 설정
        objTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
