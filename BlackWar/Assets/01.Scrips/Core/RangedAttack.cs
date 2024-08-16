using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public float speed = 10f;                      // 투사체 속도
    public float height = 1f;                      // 포물선 높이
    public Vector3 targetPosition;                 // 목표 위치
    public bool isHoming = false;                  // 유도 여부

    public GameObject targetObject;                // 목표로 할 오브젝트 (유도 투사체의 경우)

    private Vector3 startPosition;                 // 투사체의 시작 위치
    private float timeToTarget;                    // 목표까지 걸리는 시간
    private float elapsedTime = 0f;                // 경과된 시간
    private bool isLaunched = false;               // 발사 상태 확인

    // 초기화 함수
    public void Initialize(Vector3 target, float projectileSpeed, float projectileHeight, GameObject targetObj = null, bool homing = false)
    {
        startPosition = transform.position;
        targetPosition = target;
        speed = projectileSpeed;
        height = projectileHeight;
        targetObject = targetObj;
        isHoming = homing;

        // 목표까지 걸리는 시간 계산
        timeToTarget = Vector3.Distance(startPosition, targetPosition) / speed;
        elapsedTime = 0f;
        isLaunched = true; // 발사 상태로 설정
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Initialize(new Vector3(5,0,0), 5, 2);
        }

        if (isLaunched)
        {
            LaunchProjectile();
        }
    }

    // 투사체의 발사 로직
    private void LaunchProjectile()
    {
        elapsedTime += Time.deltaTime;

        // 진행 상태 계산
        float t = Mathf.Clamp01(elapsedTime / timeToTarget);

        // 유도 투사체의 경우 목표 위치 갱신
        if (isHoming && targetObject != null)
        {
            targetPosition = targetObject.transform.position;
        }

        // 포물선 경로 계산
        Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t);

        // 포물선 효과 추가
        float heightAtT = Mathf.Sin(t * Mathf.PI) * height;
        currentPos.y += heightAtT;

        // 현재 위치로 이동
        transform.position = currentPos;

        // 목표에 도달한 경우
        if (t >= 1f)
        {
            isLaunched = false;
            OnProjectileHit();
        }
    }

    // 목표에 도달했을 때 호출되는 함수
    private void OnProjectileHit()
    {
        Destroy(gameObject); // 투사체 제거
    }
}
