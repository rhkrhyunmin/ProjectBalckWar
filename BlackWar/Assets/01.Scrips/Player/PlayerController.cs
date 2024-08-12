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
    public float attackDuration = 1.0f; // ������ ���ӵǴ� �ð�
    private float attackTimer = 0.0f;

    public Transform enemy; // ���� Transform
    public float attackRange = 5.0f; // ���� ������ �Ÿ�

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
                // ���� ���°� Idle�� ���� ������ ������ �� �ֽ��ϴ�.
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
            // ���� ���¸� �����ϴ� ������ �߰��� �� �ֽ��ϴ�.
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
        if (transform.position.x > 10.0f) // ���� ��� x=10���� Idle ���·� ��ȯ
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
            // ���� �ִϸ��̼��̳� ȿ���� ���⿡ �߰��� �� �ֽ��ϴ�.
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