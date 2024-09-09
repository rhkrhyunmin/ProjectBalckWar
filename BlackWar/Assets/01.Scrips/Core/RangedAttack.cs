using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public void Fire(Vector3 startPosition, PoolableMono obj, float projectileSpeed)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        // 45�� ������ �ʱ� �߻�
        Vector2 launchVelocity = CalculateInitialVelocity(projectileSpeed);
        rb.velocity = launchVelocity;
        rb.gravityScale = 1; // �߷� Ȱ��ȭ

        // ó�� �߻� �� Z�� ȸ���� -90���� ����
        obj.transform.rotation = Quaternion.Euler(0, 0, -90);

        // �߻�ü�� ������ �� �ڿ������� ȸ���ϵ��� �ڷ�ƾ ����
        StartCoroutine(RotateWithTrajectory(obj.transform, rb));
    }

    // �߻�ü�� �̵� �� ������ ���� ȸ���ϵ��� ��
    private IEnumerator RotateWithTrajectory(Transform objTransform, Rigidbody2D rb)
    {
        while (rb.velocity.magnitude > 0.1f) // �ӵ��� ���� 0�� �Ǹ� ȸ�� �ߴ�
        {
            // ���� �ӵ��� �������� ������ ����Ͽ� �ڿ������� ȸ��
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + (-45);
            objTransform.rotation = Quaternion.Euler(0, 0, angle); // Z�� ���� ȸ��

            yield return null; // �� �����Ӹ��� ����
        }
    }

    // 45�� ������ �߻��ϴ� �ʱ� �ӵ� ���
    Vector2 CalculateInitialVelocity(float projectileSpeed)
    {
        float angle = 45 * Mathf.Deg2Rad; // 45��
        float speedX = Mathf.Cos(angle) * projectileSpeed;
        float speedY = Mathf.Sin(angle) * projectileSpeed;

        return new Vector2(speedX, speedY); // 45�� ������ �ӵ� ���� ��ȯ
    }
}
