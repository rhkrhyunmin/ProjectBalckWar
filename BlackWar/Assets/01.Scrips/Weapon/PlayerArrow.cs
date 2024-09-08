using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    [SerializeField] private float _arrowPower;

    protected Rigidbody2D _rigid;
    protected DamageCaster _damageCaster;

    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _damageCaster = GetComponent<DamageCaster>();
    }

    public virtual void Fire(Vector2 targetDir)
    {
        Vector2 normalizedDir = targetDir.normalized;

        _rigid.velocity = normalizedDir * _arrowPower;
      
        StartCoroutine(WaitForDestroy());
    }


    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        _damageCaster.ArmyRangeCastDamage();
        Destroy(gameObject);
    }
}
