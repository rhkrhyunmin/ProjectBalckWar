using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    // 발사체를 일정 시간 간격으로 발사하는 코루틴 (성에서 발사)
    public IEnumerator ShootProjectile(Transform firePoint, PoolableMono obj, Transform target, float fireRate, float projectileSpeed, bool isParabolic)
    {
        while (true)
        {
            Fire(firePoint.position, target.position, obj, projectileSpeed, isParabolic); // isParabolic 값 false로 전달
            yield return new WaitForSeconds(fireRate);
        }
    }


    void Fire(Vector3 startPosition, Vector3 targetPosition, PoolableMono obj, float projectileSpeed, bool isParabolic = false)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        Vector2 launchVelocity;

        // 포물선 운동을 사용할지 여부에 따라 발사 속도를 계산
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


    // 포물선 운동의 발사 속도를 계산하는 함수
    Vector2 CalculateParabolicVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        float displacementX = targetPosition.x - startPosition.x;
        float displacementY = targetPosition.y - startPosition.y;
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        float angle = 45 * Mathf.Deg2Rad;  // 45도 각도로 발사

        float speedX = Mathf.Cos(angle) * projectileSpeed;
        float timeToTarget = Mathf.Abs(displacementX / speedX);

        float velocityY = (displacementY + 0.5f * gravity * timeToTarget * timeToTarget) / timeToTarget;
        Vector2 launchVelocity = new Vector2(speedX, velocityY);

        return launchVelocity;
    }


    // 직선으로 발사할 때의 발사 속도를 계산하는 함수
    Vector2 CalculateStraightlineVelocity(Vector3 startPosition, Vector3 targetPosition, float projectileSpeed)
    {
        // 목표 위치로의 방향 계산
        Vector2 direction = (targetPosition - startPosition).normalized;

        // 방향 벡터에 발사 속도를 곱하여 발사 속도 벡터 생성
        Vector2 launchVelocity = direction * projectileSpeed;

        return launchVelocity;
    }

    // 발사체가 나아가는 방향으로 회전시키는 함수
    void RotateTowardsTarget(Transform objTransform, Vector2 velocity)
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        objTransform.rotation = Quaternion.Euler(0, 0, angle); // 각도 조정
    }
}