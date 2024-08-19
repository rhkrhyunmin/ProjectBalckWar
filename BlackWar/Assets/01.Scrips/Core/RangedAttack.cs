using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public void StartShooting(Transform firePoint, PoolableMono Obj, Transform target, float fireRate, float projectileSpeed)
    {
        
        StartCoroutine(ShootProjectile(firePoint, Obj, target, fireRate, projectileSpeed));
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }

    IEnumerator ShootProjectile(Transform firePoint, PoolableMono OBj, Transform target, float fireRate, float projectileSpeed)
    {
        while (true)
        {
            Fire(firePoint, OBj, target, projectileSpeed);
            yield return new WaitForSeconds(fireRate);
        }
    }

    void Fire(Transform firePoint, PoolableMono Obj, Transform target, float projectileSpeed)
    {
        //PoolManager.Instance.Pop(Obj.poolType, transform.position);
        Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();
        Vector3 launchVelocity = CalculateLaunchVelocity(firePoint.position, target.position, projectileSpeed);
        rb.velocity = launchVelocity;
    }

    Vector3 CalculateLaunchVelocity(Vector3 startPosition, Vector3 targetPosition, float speed)
    {
        Vector3 direction = targetPosition - startPosition;
        float distance = direction.magnitude;
        float heightDifference = direction.y;
        Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z);
        float horizontalDistance = horizontalDirection.magnitude;
        float angle = Mathf.Deg2Rad * 45f;
        float gravity = Physics.gravity.magnitude;
        float initialVelocity = Mathf.Sqrt(gravity * horizontalDistance * horizontalDistance / (2 * (horizontalDistance * Mathf.Tan(angle) - heightDifference)));
        Vector3 horizontalVelocity = horizontalDirection.normalized * initialVelocity;
        Vector3 verticalVelocity = Vector3.up * Mathf.Tan(angle) * initialVelocity;
        return horizontalVelocity + verticalVelocity;
    }
}

