using System.Collections;
using UnityEngine;

public class Boomerang : Magician
{
    private float speed = 5f;           // �θ޶��� �̵� �ӵ�
    
    private Vector3 startPosition;      // �θ޶��� ���۵� ��ġ

    private bool isReturning = false;   // �θ޶��� ���ƿ��� ������ ����
    private bool canThrow = true;       // �θ޶��� �ٽ� ���� �� �ִ��� ����

    protected virtual void Update()
    {
        base.Update();

        if (isReturning)
        {
            ReturnToPlayer();
        }
        else
        {
            if (CheckForAttack() && canThrow)  // ���콺 ���� ��ư���� ������
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
            transform.position += Vector3.right * speed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� �ð��� ������ ���ƿ��� ����
        isReturning = true;
    }

    void ReturnToPlayer()
    {
        // �θ޶��� ���� ��ġ�� ���ƿ��� ����
        transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

        // �θ޶��� ���� ��ġ�� �����ϸ� �ٽ� ���� �� �ֵ��� ����
        if (Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            isReturning = false;
            canThrow = true;
        }
    }
}
