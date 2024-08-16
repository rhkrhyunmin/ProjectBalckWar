using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public virtual void LaunchProjectile(Vector3 initialTarget, float projectileSpeed, float projectileHeight, PoolableMono targetObj, bool homing = false, bool isParabolic = true)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = initialTarget;
        float timeToTarget = Vector3.Distance(startPosition, targetPosition) / projectileSpeed;

        // Create an instance of the projectile from the pool
        PoolableMono projectile = PoolManager.Instance.Pop(targetObj.poolType, startPosition);
        projectile.transform.position = startPosition;

        StartCoroutine(ProjectileCoroutine(projectile, startPosition, targetPosition, timeToTarget, projectileHeight, targetObj, homing, isParabolic));
    }

    // Coroutine to manage projectile trajectory
    private IEnumerator ProjectileCoroutine(PoolableMono projectile, Vector3 startPosition, Vector3 initialTargetPosition, float timeToTarget, float height, PoolableMono targetObject, bool isHoming = false, bool isParabolic = true)
    {
        float elapsedTime = 0f;
        Vector3 currentTargetPosition = initialTargetPosition;

        while (elapsedTime < timeToTarget)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / timeToTarget);

            // Update target position if homing
            if (isHoming && targetObject != null)
            {
                currentTargetPosition = targetObject.transform.position;
            }

            // Calculate the projectile's current position
            Vector3 currentPos;
            if (isParabolic)
            {
                // Parabolic path
                currentPos = Vector3.Lerp(startPosition, currentTargetPosition, t);
                float heightAtT = Mathf.Sin(t * Mathf.PI) * height;
                currentPos.y += heightAtT;
            }
            else
            {
                // Linear path
                currentPos = Vector3.Lerp(startPosition, currentTargetPosition, t);
            }

            // Update projectile position
            projectile.transform.position = currentPos;

            yield return null;
        }

        // Ensure the projectile ends up at the target position
        projectile.transform.position = initialTargetPosition;

        OnProjectileHit(projectile);
    }

    // Method called when the projectile hits the target
    public virtual void OnProjectileHit(PoolableMono projectile)
    {
        PoolManager.Instance.Push(projectile);
    }
}
