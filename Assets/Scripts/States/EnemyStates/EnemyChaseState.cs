using UnityEngine;

class EnemyChaseState : EnemyState
{
    private const string ANIMATOR_HORIZONTAL = "horizontal";
    private const string ANIMATOR_VERTICAL = "vertical";
    private const string ANIMATOR_WALKING = "walking";

    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;

        enemy.Animator.SetBool(ANIMATOR_WALKING, true);
    }

    public void Exit()
    {
        enemy.Animator.SetBool(ANIMATOR_WALKING, false);
    }

    private Vector2 GetMovementToward(Vector2 targetPosition)
    {
        Vector2 currentPosition = enemy.transform.position;

        Vector2 direction = targetPosition - currentPosition;

        return direction.normalized;
    }

    public EnemyState FixedUpdate(float fixedDeltaTime)
    {
        Vector2 movementDirection = GetMovementToward(enemy.Target.transform.position);
        enemy.Move(movementDirection);
        enemy.Animator.SetFloat(ANIMATOR_HORIZONTAL, movementDirection.x);
        enemy.Animator.SetFloat(ANIMATOR_VERTICAL, movementDirection.y);
        return null;
    }

    public EnemyState Update(float deltaTime)
    {
        return null;
    }

    public EnemyState Input(Entity.Entity target)
    {
        if (target == null) return new EnemyIdleState();
        return null;
    }
}
