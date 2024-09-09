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

    public virtual void Fire(Transform targetDir)
    {
        // Set the velocity
        _rigid.velocity = targetDir.right * _arrowPower;

        // Rotate the arrow in the direction of the velocity
        float angle = Mathf.Atan2(_rigid.velocity.y, _rigid.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        StartCoroutine(WaitForDestroy());
    }

    void FixedUpdate()
    {
        // Continuously rotate the arrow to follow its velocity
        if (_rigid.velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(_rigid.velocity.y, _rigid.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
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
