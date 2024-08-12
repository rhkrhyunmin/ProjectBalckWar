using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Moving,
        Attacking
    }

    private State currentState;
    public float moveSpeed = 2.0f;
    public float attackDuration = 1.0f; // 공격이 지속되는 시간
    private float attackTimer = 0.0f;

    public Transform enemy; // 적의 Transform
    public float attackRange = 5.0f; // 공격 가능한 거리

    private void Start()
    {
        currentState = State.Idle;
        ChangeState(State.Moving);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // 현재 상태가 Idle일 때의 동작을 정의할 수 있습니다.
                break;

            case State.Moving:
                Move();
                CheckForAttack();
                break;

            case State.Attacking:
                Attack();
                break;
        }
    }

    private void ChangeState(State newState)
    {
        if (currentState == State.Attacking && newState != State.Attacking)
        {
            // 공격 상태를 종료하는 로직을 추가할 수 있습니다.
        }

        currentState = newState;

        if (currentState == State.Attacking)
        {
            attackTimer = attackDuration;
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if (transform.position.x > 10.0f) // 예를 들어 x=10에서 Idle 상태로 전환
        {
            ChangeState(State.Idle);
        }
    }

    private void Attack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0.0f)
        {
            ChangeState(State.Idle);
        }
        else
        {
            // 공격 애니메이션이나 효과를 여기에 추가할 수 있습니다.
            Debug.Log("Attacking!");
        }
    }

    private void CheckForAttack()
    {
        if (enemy == null) return;

        float distance = Vector2.Distance(transform.position, enemy.position);
        if (distance <= attackRange)
        {
            StartAttacking();
        }
    }

    public void StartAttacking()
    {
        if (currentState != State.Attacking)
        {
            ChangeState(State.Attacking);
        }
    }
}