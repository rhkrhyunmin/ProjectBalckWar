using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmyEntityAttackData : Army
{
    protected Army army;
    DamageCaster DamageCasterCompo => army.DamageCasterCompo;

    private void Awake()
    {
        army = GetComponent<Army>();
    }

    private void Update()
    {

    }

    public virtual void MeleeAttack(float damage)
    {
        RaycastHit2D hit = Physics2D.Raycast(army._weapon.transform.position, army._weapon.transform.right, army._armyStat.AttackDistance.GetValue(), army.enemyLayer);
        if (hit.collider != null)
        {

            Debug.Log("123");
        }
    }

    public virtual void RangerAttack(float damage)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, army._armyStat.AttackDistance.GetValue(), army.enemyLayer);

        if (hit.collider != null)
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (IsInLayerMask(hitLayer, army.enemyLayer))
            { 
                PoolManager.Instance.Push(army);
            }
           /* // 장애물을 맞췄을 경우
            else if (IsInLayerMask(hitLayer, obstacleLayer))
            {
                PoolManager.Instance.Push(this);
            }*/
        }
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) != 0);
    }
}
