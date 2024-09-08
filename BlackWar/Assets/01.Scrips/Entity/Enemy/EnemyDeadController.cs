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
        // ��� bool ������ ��ȸ�ϸ� ��Ȱ��ȭ�ϱ�
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        _anim.speed = 1f;

        //�״� �ִϸ��̼� ó��
        _anim.SetBool(HASH_DEAD, true);
        //��ƼƼ �׺�޽��� �ݶ��̴� ����
        _agent.enabled = false;
        _collider.enabled = false;
        //��ƼƼ ���� ó��
        _owner.IsDead = true;
        //��ƼƼ ��ũ��Ʈ ����
        _owner.enabled = false;
    }
}
