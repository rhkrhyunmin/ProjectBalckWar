using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    // 발사체 발사를 시작하는 함수
    public void StartShooting(Transform firePoint, PoolableMono Obj, Transform target, float fireRate, float projectileSpeed)
    {
        StartCoroutine(ShootProjectile(firePoint, Obj, target, fireRate, projectileSpeed));
    }

    // 발사체 발사를 멈추는 함수
    public void StopShooting()
    {
        StopAllCoroutines();
    }

    // 발사체를 일정 시간 간격으로 발사하는 코루틴
    IEnumerator ShootProjectile(Transform firePoint, PoolableMono Obj, Transform target, float fireRate, float projectileSpeed)
    {
        while (true)
        {
            Fire(firePoint, Obj, target, projectileSpeed);
            yield return new WaitForSeconds(fireRate);
        }
    }

    // 발사체를 발사하는 함수
    void Fire(Transform firePoint, PoolableMono Obj, Transform target, float projectileSpeed)
    {
        // 발사체 인스턴스 생성
        Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();

        // 발사 속도 계산 (포물선 운동)
        Vector2 launchVelocity = CalculateLaunchVelocity(firePoint.position, target.position, projectileSpeed);

        // Rigidbody2D의 속도 설정
        rb.velocity = launchVelocity;

        // 발사체의 회전을 초기 속도에 맞춰 설정 (첫 발사 시)
        RotateTowardsTarget(Obj.transform, rb.velocity);

        // 발사체가 계속해서 진행 방향으로 회전하도록 함
        Obj.StartCoroutine(UpdateRotation(Obj.transform, rb));
    }

    // 포물선 운동의 발사 속도를 계산하는 함수
    Vector2 CalculateLaunchVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        // 수평 및 수직 거리 계산
        float displacementX = targetPosition.x - startPosition.x;
        float displacementY = targetPosition.y - startPosition.y;

        // 중력 가속도 (2D 환경에서 사용)
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // 발사 각도 (45도를 가정하거나, 원하는 각도로 설정 가능)
        float angle = 45 * Mathf.Deg2Rad;

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

    // 발사체가 나아가는 방향으로 회전시키는 함수
    void RotateTowardsTarget(Transform objTransform, Vector2 velocity)
    {
        // 발사체가 나아가는 방향으로 회전 (Transform.up이 속도 벡터와 일치하도록 함)
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        // 발사체의 up 방향을 속도 벡터와 일치시키도록 회전 설정
        objTransform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    // 발사체가 진행 중일 때 계속 회전하도록 하는 코루틴
    IEnumerator UpdateRotation(Transform objTransform, Rigidbody2D rb)
    {
        while (true)
        {
            // 속도 벡터가 있는 경우에만 회전
            if (rb.velocity != Vector2.zero)
            {
                RotateTowardsTarget(objTransform, rb.velocity);
            }

            yield return null; // 다음 프레임까지 대기
        }
    }
}
