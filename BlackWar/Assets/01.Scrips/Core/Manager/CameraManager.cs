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
        if (camera.transform.position.x < MaxDictance && camera.transform.position.x > MinDictance)
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
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("123");
                MoveCamera(-moveSpeed * Time.deltaTime);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("123");
                MoveCamera(moveSpeed * Time.deltaTime);
            }
        }
    }

    void MoveCamera(float amount)
    {
        camera.transform.Translate(new Vector3(amount, 0, 0), Space.World);
    }
}
