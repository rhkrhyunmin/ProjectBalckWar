using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmyEntityAttackData : MonoBehaviour
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

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            Army weaponArmy = other.GetComponentInParent<Army>();

            if (weaponArmy != null)
            {
                float attackPower = weaponArmy._armyStat.AttackPower.GetValue();
                army.OnHit(attackPower);
            }
        }
    }

    public virtual void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
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
        }
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) != 0);
    }
}
