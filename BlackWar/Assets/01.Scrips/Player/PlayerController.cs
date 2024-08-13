using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Moving,
        Attacking,
        Hitting,
        Die
    }

    public PlayerStat stat;
    private State currentState;

    public LayerMask enemyLayer;

    public float CurrentHp;

    //private float attackTimer;
    private float attackCooldownTimer;

    [HideInInspector]
    public bool isWalk;
    public bool isAttack;
    public bool isHit;
    public bool isDie;

    private void Start()
    {
        currentState = State.Idle;
        //attackTimer = 0.0f;
        attackCooldownTimer = 0.0f; // 쿨다운 타이머 초기화
        ChangeState(State.Moving); // 초기 상태를 Moving으로 설정
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // Idle 상태에서 별도의 동작 없음
                break;

            case State.Moving:
                Move();
                CheckForAttack();
                break;

            case State.Hitting:
                OnHit();
                break;

            case State.Attacking:
                Attack();
                break;

            case State.Die:
                // 죽음 상태에서 별도의 동작 없음
                break;
        }

        // 쿨다운 타이머 업데이트
        if (attackCooldownTimer > 0.0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }

    private void ChangeState(State newState)
    {
        if (currentState == State.Attacking && newState != State.Attacking)
        {
            isAttack = true;
        }

        currentState = newState;

        if (currentState == State.Attacking)
        {
            isAttack = false;
        }
    }

    private void Move()
    {
        if (currentState == State.Moving)
        {
            transform.Translate(Vector2.right * stat.MoveSpeed.GetValue() * Time.deltaTime);

            // 특정 위치에서 Idle 상태로 전환
            if (transform.position.x > 10.0f)
            {
                ChangeState(State.Idle);
            }
        }
    }

    private void Attack()
    {
        if (!isAttack)
        {
            ChangeState(State.Attacking);
            isAttack = true;
            Debug.Log("123");
        }
        else
        {
            ChangeState(State.Idle);
            attackCooldownTimer = stat.AttackDelay.GetValue(); // 쿨다운 시간 설정
        }
    }

    private void CheckForAttack()
    {
        if (currentState == State.Moving && attackCooldownTimer <= 0.0f)
        {
            // 적 탐지 범위 내에서 적이 있는지 확인
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, stat.AttackDistance.GetValue(), enemyLayer);

            if (enemies.Length > 0)
            {
                Attack();
            }
            else
            {
                isAttack = false;
            }
        }
    }

   /* private void StartAttacking()
    {
        if (currentState != State.Attacking)
        {
            ChangeState(State.Attacking);
        }
    }*/

    public void OnHit()
    {
        if (CurrentHp <= 0)
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        if (isDie)
        {
            // PoolManager.Instance.Push();
            Debug.Log("Player died.");
        }
    }
}
