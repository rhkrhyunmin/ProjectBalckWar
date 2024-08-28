using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 무기 타입 열거형 정의


public class WeaponBrain : PoolableMono
{
    // 무기 타입 설정
    
    protected State currentState;

    // 미리 정의된 레이어 상수
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public PlayerController playerController;

}
