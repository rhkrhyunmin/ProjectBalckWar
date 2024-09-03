using System.Collections;
using UnityEngine;

public class Boomerang : Magician
{
    private float speed = 5f;           // 부메랑의 이동 속도
    
    private Vector3 startPosition;      // 부메랑이 시작된 위치

    private bool isReturning = false;   // 부메랑이 돌아오는 중인지 여부
    private bool canThrow = true;       // 부메랑을 다시 던질 수 있는지 여부

    protected virtual void Update()
    {
        base.Update();

        if (isReturning)
        {
            ReturnToPlayer();
        }
        else
        {
            if (CheckForAttack() && canThrow)  // 마우스 왼쪽 버튼으로 던지기
            {
                ThrowBoomerang();
            }
        }

        RotationObj(360);
    }

    public void RotationObj(float speed)
    {
        // 부메랑의 회전 (Z축을 기준으로 회전)
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

        // 일정 시간이 지나면 돌아오기 시작
        isReturning = true;
    }

    void ReturnToPlayer()
    {
        // 부메랑이 시작 위치로 돌아오는 로직
        transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

        // 부메랑이 시작 위치에 도달하면 다시 던질 수 있도록 설정
        if (Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            isReturning = false;
            canThrow = true;
        }
    }
}
