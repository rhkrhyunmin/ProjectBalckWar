using System.Collections;
using UnityEngine;

public class Boomerang : MonoBehaviour
{   
    private Vector3 startPosition;
    public Ninja ninja;

    private bool isReturning = false;   
    private bool canThrow = true;       

    private void Update()
    {

        if (isReturning)
        {
            ReturnToPlayer();
        }
        else
        {
            if (ninja.CheckForAttack() && canThrow)  // ���콺 ���� ��ư���� ������
            {
                ThrowBoomerang();
            }
        }

        RotationObj(360);
    }

    public void RotationObj(float speed)
    {
        // �θ޶��� ȸ�� (Z���� �������� ȸ��)
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    void ThrowBoomerang()
    {
        startPosition = transform.position;
        isReturning = false;
        canThrow = false;
        StartCoroutine(ThrowAndReturn());
    }

    IEnumerator ThrowAndReturn()
    {
        float travelTime = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < travelTime)
        {
            transform.position += Vector3.right * ninja._armyStat.AttackTimer.GetValue() * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� �ð��� ������ ���ƿ��� ����
        isReturning = true;
    }

    void ReturnToPlayer()
    {
        // �θ޶��� ���� ��ġ�� ���ƿ��� ����
        transform.position = Vector3.MoveTowards(transform.position, startPosition, ninja._armyStat.AttackTimer.GetValue() * Time.deltaTime);

        // �θ޶��� ���� ��ġ�� �����ϸ� �ٽ� ���� �� �ֵ��� ����
        if (Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            isReturning = false;
            canThrow = true;
        }
    }
}
