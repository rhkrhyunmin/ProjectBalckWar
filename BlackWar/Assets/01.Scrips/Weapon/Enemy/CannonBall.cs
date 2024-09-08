using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float _caonnonBallPower;

    protected Rigidbody2D _rigid;
    protected DamageCaster _damageCaster;

    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _damageCaster = GetComponent<DamageCaster>();
    }

    public virtual void Fire(Transform targetDir)
    {
        _rigid.velocity = -targetDir.right * _caonnonBallPower;
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _damageCaster.EnemyRangeCastDamage();
        Destroy(gameObject);
    }
}
