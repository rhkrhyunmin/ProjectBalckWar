using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Quaternion rotation;

    public Quaternion Rotation
    {
        get { return rotation; }
        set
        {
            // Y�� ȸ���� ������ ���¿��� X�� Z�� ȸ���� ����
            rotation = Quaternion.Euler(value.eulerAngles.x, rotation.eulerAngles.y, value.eulerAngles.z);

            // ������Ʈ�� ����
            transform.rotation = rotation;
        }
    }

    void Start()
    {
        // �ʱ� ȸ������ ����
        rotation = transform.rotation;
    }

}
