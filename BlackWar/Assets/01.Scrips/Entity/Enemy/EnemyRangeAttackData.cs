using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackData : EnemyEntityAttackData
{
    [Header("RangeAttack Info")]
    [SerializeField] protected CannonBall _cannonBallPrefab;
    [SerializeField] protected Transform _firePos;
    [SerializeField] LayerMask enemyLayer;

    public override void CanonRangeAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(_firePos.position, -_firePos.right, Mathf.Infinity, enemyLayer);

        if (hit.collider != null)
        {
            //Vector2 targetDir = _firePos.transform.position - hit.transform.position;
            Transform targetTrm = hit.transform;

            CannonBall cannonBall = Instantiate(_cannonBallPrefab, _firePos.position, _firePos.rotation);

            cannonBall.Fire(targetTrm);
        }
    }
}
