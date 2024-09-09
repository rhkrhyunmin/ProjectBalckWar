using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeadController<T> : PoolableMono, IDeadable where T : Enemy
{
    protected readonly int HASH_DEAD = Animator.StringToHash("Dead");

    protected T _owner;

    protected Animator _anim;
    protected NavMeshAgent _agent;
    protected Collider _collider;

    protected virtual void Awake()
    {
        _owner = GetComponent<T>();
        _collider = GetComponent<Collider>();
        _anim = transform.Find("Visual").GetComponent<Animator>();
    }

    public void OnDied()
    {
        // 모든 bool 변수를 순회하며 비활성화하기
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        _anim.speed = 1f;

        //죽는 애니메이션 처리
        _anim.SetBool(HASH_DEAD, true);
        //엔티티 네브메쉬와 콜라이더 꺼줌
        _agent.enabled = false;
        _collider.enabled = false;
        //엔티티 죽음 처리
        _owner.IsDead = true;
        //엔티티 스크립트 꺼줌
        _owner.enabled = false;
    }
}
