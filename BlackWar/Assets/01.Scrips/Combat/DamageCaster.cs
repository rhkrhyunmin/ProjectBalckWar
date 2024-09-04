using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public LayerMask TargetLayer;

    public void SetPostion()
    {
        transform.localPosition = Vector3.zero;
    }

    //공격들 구현

    //기본공격
    public void CastDamage()
    {

    }
}
