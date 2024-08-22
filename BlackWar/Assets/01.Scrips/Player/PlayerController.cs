using UnityEngine;
using System.IO;

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
    [HideInInspector]
    public bool isAttack;
    [HideInInspector]
    public bool isHit;
    [HideInInspector]
    public bool isDie;

    private void Start()
    {
        CurrentHp = stat.MaxHp.GetValue();
        currentState = State.Idle;
        attackCooldownTimer = 0.0f;
        ChangeState(State.Moving); // 초기 상태를 Moving으로 설정
    }

    private void Update()
    {
        HandleState();

        // 쿨다운 타이머 업데이트
        if (attackCooldownTimer > 0.0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.E)) 
        {
            stat.MaxHp.AddModifier(10f);
            Debug.Log("10");
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
        // 상태 전환 규칙에 따라 상태 변경 허용 여부 결정
        if (currentState == newState) return; // 같은 상태는 무시

        // 상태 변경
        currentState = newState;
        UpdateStateFlags();

        // Attack 상태로 변경 시 이동 중지
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
        isAttack = true;
        if (attackCooldownTimer <= 0.0f)
        {
            // 공격 로직 처리 후, 공격 쿨다운 설정
            attackCooldownTimer = stat.AttackDelay.GetValue();
            isAttack = false;
        }
    }

    private void CheckForAttack()
    {
        if (currentState == State.Moving && !isAttack)
        {
            // 적 탐지 범위 내에서 적이 있는지 확인
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
            PoolManager.Instance.Push(this); // 필요시 활성화
        }
    }
}
