using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public void Fire(Vector3 startPosition, PoolableMono obj, float projectileSpeed)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        obj.transform.position = startPosition; // Set the initial position
        obj.transform.rotation = Quaternion.Euler(0, 0, -90); // Set initial rotation

        // Calculate initial velocity with a boost
        Vector2 launchVelocity = CalculateInitialVelocity(projectileSpeed);
        rb.velocity = launchVelocity;
        rb.gravityScale = 1; // Activate gravity

        // Start coroutine for smooth rotation based on trajectory
        StartCoroutine(RotateWithTrajectory(obj.transform, rb));
    }

    private IEnumerator RotateWithTrajectory(Transform objTransform, Rigidbody2D rb)
    {
        while (rb.velocity.magnitude > 0.1f)
        {
            // Calculate target angle based on current velocity
            float targetAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // Smoothly rotate the projectile towards the target angle
            objTransform.rotation = Quaternion.Lerp(objTransform.rotation, targetRotation, Time.deltaTime * 10f);

            yield return null;
        }
    }

    private Vector2 CalculateInitialVelocity(float projectileSpeed)
    {
        // Initial boost for the projectile
        float boostFactor = 1.5f; // Adjust this factor for more/less initial speed
        float angle = 45 * Mathf.Deg2Rad; // 45 degrees to radians

        // Calculate initial velocity components
        float speedX = Mathf.Cos(angle) * projectileSpeed * boostFactor;
        float speedY = Mathf.Sin(angle) * projectileSpeed * boostFactor;

        return new Vector2(speedX, speedY);
    }
}
