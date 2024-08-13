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
        attackCooldownTimer = 0.0f; // ��ٿ� Ÿ�̸� �ʱ�ȭ
        ChangeState(State.Moving); // �ʱ� ���¸� Moving���� ����
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // Idle ���¿��� ������ ���� ����
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
                // ���� ���¿��� ������ ���� ����
                break;
        }

        // ��ٿ� Ÿ�̸� ������Ʈ
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

            // Ư�� ��ġ���� Idle ���·� ��ȯ
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
            attackCooldownTimer = stat.AttackDelay.GetValue(); // ��ٿ� �ð� ����
        }
    }

    private void CheckForAttack()
    {
        if (currentState == State.Moving && attackCooldownTimer <= 0.0f)
        {
            // �� Ž�� ���� ������ ���� �ִ��� Ȯ��
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
