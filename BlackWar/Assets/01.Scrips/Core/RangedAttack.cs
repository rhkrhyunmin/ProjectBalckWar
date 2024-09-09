using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public void Fire(Vector3 startPosition, PoolableMono obj, float projectileSpeed)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        // 45도 각도로 초기 발사
        Vector2 launchVelocity = CalculateInitialVelocity(projectileSpeed);
        rb.velocity = launchVelocity;
        rb.gravityScale = 1; // 중력 활성화

        // 처음 발사 시 Z축 회전을 -90도로 고정
        obj.transform.rotation = Quaternion.Euler(0, 0, -90);

        // 발사체가 떨어질 때 자연스럽게 회전하도록 코루틴 실행
        StartCoroutine(RotateWithTrajectory(obj.transform, rb));
    }

    // 발사체가 이동 중 궤적에 맞춰 회전하도록 함
    private IEnumerator RotateWithTrajectory(Transform objTransform, Rigidbody2D rb)
    {
        while (rb.velocity.magnitude > 0.1f) // 속도가 거의 0이 되면 회전 중단
        {
            // 현재 속도를 기준으로 각도를 계산하여 자연스럽게 회전
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + (-45);
            objTransform.rotation = Quaternion.Euler(0, 0, angle); // Z축 기준 회전

            yield return null; // 매 프레임마다 실행
        }
    }

    // 45도 각도로 발사하는 초기 속도 계산
    Vector2 CalculateInitialVelocity(float projectileSpeed)
    {
        float angle = 45 * Mathf.Deg2Rad; // 45도
        float speedX = Mathf.Cos(angle) * projectileSpeed;
        float speedY = Mathf.Sin(angle) * projectileSpeed;

        return new Vector2(speedX, speedY); // 45도 각도의 속도 벡터 반환
    }
}
