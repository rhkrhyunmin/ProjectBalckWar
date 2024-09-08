using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera camera;
    public float moveSpeed = 5f;
    public float dragSpeed = 0.1f;

    public float MaxDictance;
    public float MinDictance;

    private Vector2 dragOrigin;

    void Update()
    {
        // 카메라의 x 좌표가 MaxDistance + 3 이하이면서 MinDistance - 3 이상인 경우에만 이동 가능
        if (camera.transform.position.x <= MaxDictance + 3 && camera.transform.position.x >= MinDictance - 3)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    dragOrigin = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 difference = touch.position - dragOrigin;
                    MoveCamera(-difference.x * dragSpeed);
                    dragOrigin = touch.position;
                }
            }

            // 키보드 방향키 처리
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("123");
                MoveCamera(moveSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("123");
                MoveCamera(-moveSpeed * Time.deltaTime);
            }
        }
    }

    void MoveCamera(float deltaX)
    {
        // 카메라의 현재 위치를 가져옴
        Vector3 newPosition = camera.transform.position;

        // 새 위치 계산
        newPosition.x += deltaX;

        // 카메라의 위치를 MaxDistance와 MinDistance 사이로 제한
        newPosition.x = Mathf.Clamp(newPosition.x, MinDictance, MaxDictance);

        // 새로운 위치 적용
        camera.transform.position = newPosition;
    }

}
