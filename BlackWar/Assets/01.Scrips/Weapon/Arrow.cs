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
            // Y축 회전을 고정한 상태에서 X와 Z축 회전만 변경
            rotation = Quaternion.Euler(value.eulerAngles.x, rotation.eulerAngles.y, value.eulerAngles.z);

            // 오브젝트에 적용
            transform.rotation = rotation;
        }
    }

    void Start()
    {
        // 초기 회전값을 설정
        rotation = transform.rotation;
    }

}
