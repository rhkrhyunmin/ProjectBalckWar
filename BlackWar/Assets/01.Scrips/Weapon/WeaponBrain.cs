using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� Ÿ�� ������ ����


public class WeaponBrain : PoolableMono
{
    // ���� Ÿ�� ����
    
    protected State currentState;

    // �̸� ���ǵ� ���̾� ���
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public PlayerController playerController;

}
