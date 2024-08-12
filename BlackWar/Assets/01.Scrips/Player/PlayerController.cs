using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Moving,
        Attacking
    }

    public PlayerStat stat;
    private State currentState;

    public LayerMask enemyLayer; // 적이 포함된 레이어
    public float attackRange = 5.0f; // 공격 범위

    private float attackTimer; // 공격의 남은 시간

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
            attackTimer = stat.AttackDelay.GetValue(); // 공격 시작 시 타이머를 AttackDelay로 초기화
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.right * stat.MoveSpeed.GetValue() * Time.deltaTime);
        if (transform.position.x > 10.0f) // 예를 들어 x=10에서 Idle 상태로 전환
        {
            ChangeState(State.Idle);
        }
    }

    private void Attack()
    {
        attackTimer -= Time.deltaTime; // 공격 시간이 경과함에 따라 감소
        if (attackTimer <= 0.0f)
        {
            ChangeState(State.Idle); // 공격이 끝나면 Idle 상태로 전환
        }
        else
        {
            // 공격 애니메이션이나 효과를 여기에 추가할 수 있습니다.
            Debug.Log("Attacking!");
        }
    }

    private void CheckForAttack()
    {
        // 적 탐지 범위 내에서 적이 있는지 확인
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, stat.AttackDistance.GetValue(), enemyLayer);

        if (enemies.Length > 0)
        {
            Debug.Log("Enemy detected within attack range.");
            StartAttacking();
        }
        else
        {
            Debug.Log("No enemies detected within attack range.");
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
