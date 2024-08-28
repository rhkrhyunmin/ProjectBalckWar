using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerStatData
{
    public float MaxHp;
    public float MoveSpeed;
    public float AttackDelay;
    public float AttackDistance;

    public PlayerStatData(PlayerStat stat)
    {
        MaxHp = stat.MaxHp.GetValue();
        MoveSpeed = stat.MoveSpeed.GetValue();
        AttackDelay = stat.AttackDelay.GetValue();
        AttackDistance = stat.AttackDistance.GetValue();
    }
}

public enum State
{
    Idle,
    Attacking,
    Hitting,
    Moving,
    Die
}

public class PlayerController : PoolableMono
{
    public PlayerStat stat;

    private State currentState;
    private PlayerAnimation anmation;

    public LayerMask enemyLayer;

    public float CurrentHp;

    private float attackCooldownTimer;
    private Animator animator;

    [HideInInspector]
    public bool isWalk;
    [HideInInspector]
    public bool isAttack;
    [HideInInspector]
    public bool isHit;
    [HideInInspector]
    public bool isDie;

    private void Awake()
    {
        anmation = GetComponent<PlayerAnimation>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CurrentHp = stat.MaxHp.GetValue();
        currentState = State.Idle;
        attackCooldownTimer = 0.0f;

        ChangeState(State.Moving); 
    }

    private void Update()
    {
        HandleState();

        // ��ٿ� Ÿ�̸� ������Ʈ
        if (attackCooldownTimer > 0.0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            stat.MaxHp.AddModifier(10f);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeState(State.Hitting);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SaveStatToJson();
        }
    }

    public void SaveStatToJson()
    {
        PlayerStatData data = new PlayerStatData(stat);
        string json = JsonUtility.ToJson(data, true);

        string path = Path.Combine(Application.persistentDataPath, "player_stat.json");

        File.WriteAllText(path, json);

        Debug.Log("Player stats saved to JSON: " + data.MaxHp);
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
                Attack(stat.AttackPower.GetValue());
                break;

            case State.Die:
                OnDie();
                break;
        }
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

        anmation.SetState(currentState);
    }

    private void Move()
    {
        if (currentState == State.Moving)
        {
            transform.Translate(Vector2.right * stat.MoveSpeed.GetValue() * Time.deltaTime);
            
        }
    }

    public virtual void Attack(float damage)
    {
        isAttack = true;
        if (attackCooldownTimer <= 0.0f)
        {
            // ���� ���� ó�� ��, ���� ��ٿ� ����
            attackCooldownTimer = stat.AttackDelay.GetValue();
            isAttack = false;
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
                Attack(stat.AttackPower.GetValue());
            }
        }
    }

    public void OnHit(float damage)
    {
        CurrentHp -= damage;
        ChangeState(State.Hitting);
        Debug.Log(CurrentHp);
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
            PoolManager.Instance.Push(this);
        }
    }
}
