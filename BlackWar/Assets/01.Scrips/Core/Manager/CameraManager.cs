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
        // ī�޶��� x ��ǥ�� MaxDistance + 3 �����̸鼭 MinDistance - 3 �̻��� ��쿡�� �̵� ����
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

            // Ű���� ����Ű ó��
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
        // ī�޶��� ���� ��ġ�� ������
        Vector3 newPosition = camera.transform.position;

        // �� ��ġ ���
        newPosition.x += deltaX;

        // ī�޶��� ��ġ�� MaxDistance�� MinDistance ���̷� ����
        newPosition.x = Mathf.Clamp(newPosition.x, MinDictance, MaxDictance);

        // ���ο� ��ġ ����
        camera.transform.position = newPosition;
    }

}
