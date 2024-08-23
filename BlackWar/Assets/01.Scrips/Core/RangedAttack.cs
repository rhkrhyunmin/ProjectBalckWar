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
        Vector3 launchVelocity = CalculateLaunchVelocity(firePoint.position, target.position, projectileSpeed, Obj);
        rb.velocity = launchVelocity;
    }

    Vector3 CalculateLaunchVelocity(Vector3 startPosition, Vector3 targetPosition, float speed, PoolableMono Obj)
    {
        Vector3 direction = targetPosition - startPosition;
        float distance = direction.magnitude;
        float heightDifference = direction.y;
        Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z);
        float horizontalDistance = horizontalDirection.magnitude;
        float angle = Mathf.Deg2Rad * 45f;
        float gravity = Physics.gravity.magnitude;

        float initialVelocity = Mathf.Sqrt(gravity * horizontalDistance * horizontalDistance /
            (2 * (horizontalDistance * Mathf.Tan(angle) - heightDifference)));

        Vector3 horizontalVelocity = horizontalDirection.normalized * initialVelocity;
        Vector3 verticalVelocity = Vector3.up * Mathf.Tan(angle) * initialVelocity;

        // Calculate the final launch velocity
        Vector3 launchVelocity = horizontalVelocity + verticalVelocity;

        // Make sure the object is always oriented correctly
        if (launchVelocity != Vector3.zero)
        {
            // Calculate the direction of the target
            Quaternion targetRotation = Quaternion.LookRotation(launchVelocity);

            // Check if the object is facing too far downward (for example, if y-axis is pointing too low)
            float currentPitch = targetRotation.eulerAngles.x; // This gives the X rotation angle (pitch)

            // Set a minimum and maximum pitch range to prevent too steep angles
            float minPitch = -60f;  // Prevent the object from tilting downward too much
            float maxPitch = 60f;   // Prevent the object from tilting upward too much

            // Clamp the pitch (X rotation) to stay within limits
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

            // Apply the clamped pitch back to the rotation
            targetRotation = Quaternion.Euler(currentPitch, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

            // Smoothly interpolate between the current rotation and the target rotation for smoother transitions
            Obj.transform.rotation = Quaternion.Slerp(Obj.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        return launchVelocity;
    }



}

