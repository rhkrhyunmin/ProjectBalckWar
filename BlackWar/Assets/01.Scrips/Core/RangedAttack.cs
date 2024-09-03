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
        // Quaternion을 사용하여 회전 행렬 생성
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector2 rotatedVector = rotation * vector;

        return rotatedVector;
    }

    // 포물선 운동의 발사 속도를 계산하는 함수
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

    // Rigidbody2D 초기화
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

    // 발사체가 나아가는 방향으로 회전시키는 함수
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
