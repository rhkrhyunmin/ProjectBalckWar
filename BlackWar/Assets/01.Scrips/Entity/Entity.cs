using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : PoolableMono
{
    public LayerMask enemyLayer;
    public LayerMask castleLayer;

    public Animator AnimatorCompo { get; protected set; }
    public DamageCaster DamageCasterCompo { get; protected set; }

    protected virtual void Awake()
    {
        AnimatorCompo = GetComponentInChildren<Animator>();
        DamageCasterCompo = GetComponentInChildren<DamageCaster>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {

    }

    public virtual void OnDie()
    {
        //PoolManager.Instance.Push(this);
    }
}
