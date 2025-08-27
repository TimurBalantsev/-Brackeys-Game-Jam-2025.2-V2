using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public event Action<Entity.Entity> OnTargetSpotted;
    public event Action OnTargetLost;

    [SerializeField] private float radius = 5f;
    [SerializeField, Range(0f, 360f)] private float angle = 5f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstructionLayer;

    private Entity.Entity currentTarget;

    private void Update()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        Entity.Entity foundTarget = null;

        foreach (Collider2D col in rangeCheck)
        {
            Transform target = col.transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    foundTarget = target.GetComponent<Entity.Entity>();
                    break;
                }
            }
        }

        if (foundTarget != null)
        {
            if (currentTarget == null)
            {
                currentTarget = foundTarget;
                OnTargetSpotted?.Invoke(foundTarget);
            }
        }
        else if (currentTarget != null)
        {
            currentTarget = null;
            OnTargetLost?.Invoke();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + angle1 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle2 * radius);
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
#endif
}
