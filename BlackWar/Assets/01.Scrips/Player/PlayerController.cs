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

    public LayerMask enemyLayer; // ���� ���Ե� ���̾�
    public float attackRange = 5.0f; // ���� ����

    private float attackTimer; // ������ ���� �ð�

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
            // ���� ���¸� �����ϴ� ������ �߰��� �� �ֽ��ϴ�.
        }

        currentState = newState;

        if (currentState == State.Attacking)
        {
            attackTimer = stat.AttackDelay.GetValue(); // ���� ���� �� Ÿ�̸Ӹ� AttackDelay�� �ʱ�ȭ
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.right * stat.MoveSpeed.GetValue() * Time.deltaTime);
        if (transform.position.x > 10.0f) // ���� ��� x=10���� Idle ���·� ��ȯ
        {
            ChangeState(State.Idle);
        }
    }

    private void Attack()
    {
        attackTimer -= Time.deltaTime; // ���� �ð��� ����Կ� ���� ����
        if (attackTimer <= 0.0f)
        {
            ChangeState(State.Idle); // ������ ������ Idle ���·� ��ȯ
        }
        else
        {
            // ���� �ִϸ��̼��̳� ȿ���� ���⿡ �߰��� �� �ֽ��ϴ�.
            Debug.Log("Attacking!");
        }
    }

    private void CheckForAttack()
    {
        // �� Ž�� ���� ������ ���� �ִ��� Ȯ��
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
