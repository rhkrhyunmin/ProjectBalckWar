using UnityEngine;

public class PlayerController : PlayerBrain
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

    private float attackCooldownTimer;

    [HideInInspector]
    public bool isWalk;
    public bool isAttack;
    public bool isHit;
    public bool isDie;

    private void Start()
    {
        CurrentHp = stat.MaxHp.GetValue();
        currentState = State.Idle;
        attackCooldownTimer = 0.0f;
        ChangeState(State.Moving); // �ʱ� ���¸� Moving���� ����
    }

    private void Update()
    {
        HandleState();

        // ��ٿ� Ÿ�̸� ������Ʈ
        if (attackCooldownTimer > 0.0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeState(State.Hitting);
        }
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case State.Idle:
                CheckForAttack();
                break;

            case State.Moving:
                Move();
                CheckForAttack();
                break;

            case State.Hitting:
                OnHit(5f);
                break;

            case State.Attacking:
                Attack();
                break;

            case State.Die:
                OnDie();
                break;
        }
       // Debug.Log(isAttack);
    }

    private void ChangeState(State newState)
    {
        // ���� ��ȯ ��Ģ�� ���� ���� ���� ��� ���� ����
        if (currentState == newState) return; // ���� ���´� ����

        // ���� ����
        currentState = newState;
        UpdateStateFlags();

        // Attack ���·� ���� �� �̵� ����
        if (newState == State.Attacking)
        {
            isWalk = false;
        }
    }

    private void UpdateStateFlags()
    {
        isAttack = currentState == State.Attacking;
        isWalk = currentState == State.Moving;
        isHit = currentState == State.Hitting;
        isDie = currentState == State.Die;
        Debug.Log(currentState);
    }

    private void Move()
    {
        if (currentState == State.Moving)
        {
            transform.Translate(Vector2.right * stat.MoveSpeed.GetValue() * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (attackCooldownTimer <= 0.0f)
        {
            // ���� ���� ó�� ��, ���� ��ٿ� ����
            attackCooldownTimer = stat.AttackDelay.GetValue();
            //ChangeState(State.Moving); // ���� �� Moving���� ��ȯ
        }
    }

    private void CheckForAttack()
    {
        if (currentState == State.Moving && !isAttack)
        {
            // �� Ž�� ���� ������ ���� �ִ��� Ȯ��
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, stat.AttackDistance.GetValue(), enemyLayer);

            if (enemies.Length > 0)
            {
                ChangeState(State.Attacking);
                Attack();
            }
        }
    }

    public void OnHit(float damage)
    {
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            ChangeState(State.Die);
            isDie = true;
        }
        else
        {
            ChangeState(State.Idle);
        }

    }

    private void OnDie()
    {
        if (isDie)
        {
            PoolManager.Instance.Push(this); // �ʿ�� Ȱ��ȭ
        }
    }
}
