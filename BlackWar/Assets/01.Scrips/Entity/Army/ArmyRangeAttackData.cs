using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyRangeAttackData : ArmyEntityAttackData
{
    [Header("RangeAttack Info")]
    [SerializeField] protected PlayerArrow _arrow;
    [SerializeField] protected Transform _firePos;
    [SerializeField] LayerMask enemyLayer;

    public override void RangerAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(_firePos.position, _firePos.right, Mathf.Infinity, enemyLayer);

        if (hit.collider != null)
        {
            Vector2 targetDir = _firePos.transform.position - hit.transform.position;

            PlayerArrow _arrowPrefab = Instantiate(_arrow, _firePos.position, _firePos.rotation);
            Debug.Log("5");
            _arrowPrefab.Fire(targetDir);
        }
    }
}
