using UnityEngine;
using UnityEngine.Tilemaps;

class EnemyPatrolState : EnemyState
{
    private const string ANIMATOR_HORIZONTAL = "horizontal";
    private const string ANIMATOR_VERTICAL = "vertical";
    private const string ANIMATOR_WALKING = "walking";

    private Enemy enemy;
    private Vector2 targetPosition;

    private float patrolTimer;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        targetPosition = GetNextTargetPosition();
        enemy.Animator.SetBool(ANIMATOR_WALKING, true);
    }

    public void Exit()
    {
        enemy.Animator.SetBool(ANIMATOR_WALKING, false);
    }

    public EnemyState Update(float deltaTime)
    {
        if (enemy.Target != null)
        {
            return new EnemyChasingState();
        }

        patrolTimer += deltaTime;

        // If the enemy patrolled for longer than its patrolDuration, return back to idle state
        if (patrolTimer >= enemy.patrolDuration)
        {
            return new EnemyPatrolState();
        }

        return null;
    }

    private Vector2 GetNextTargetPosition()
    {
        Vector2 currentPosition = enemy.transform.position;
        Vector2 randomPosition;
        int iterations = 0;

        do
        {
            float x = currentPosition.x + Random.Range(-enemy.maxPatrolDistance, enemy.maxPatrolDistance + 1);
            float y = currentPosition.y + Random.Range(-enemy.maxPatrolDistance, enemy.maxPatrolDistance + 1);
            randomPosition = new Vector2(x, y);

            iterations++;
            if (iterations > 1000)
            {
                Debug.LogWarning("Could not find a nearby free tile!");
                return enemy.transform.position;
            }

        }
        while (!IsPositionFree(randomPosition));

        return randomPosition;
    }

    public bool IsPositionFree(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapPoint(position, enemy.ObstacleMask);
        return hit == null;
    }

    private Vector2 GetMovementToward(Vector2 targetPosition)
    {
        Vector2 currentPosition = enemy.transform.position;

        Vector2 direction = targetPosition - currentPosition;

        return direction.normalized;
    }

    public EnemyState FixedUpdate(float fixedDeltaTime)
    {
        Vector2 currentPosition = enemy.transform.position;
        float distance = Vector2.Distance(currentPosition, targetPosition);

        // If the enemy reached the target tile, return back to idle state
        if (distance <= 0.1f)
        {
            return new EnemyIdleState();
        }

        Vector2 movementDirection = GetMovementToward(targetPosition);
        enemy.Move(movementDirection);
        enemy.Animator.SetFloat(ANIMATOR_HORIZONTAL, movementDirection.x);
        enemy.Animator.SetFloat(ANIMATOR_VERTICAL, movementDirection.y);
        return null;
    }

    public EnemyState Input(Entity.Entity target)
    {
        if (target != null) return new EnemyChasingState();
        return null;
    }
}
