using UnityEngine;

class EnemyAttackingState : EnemyState
{
    private const string ANIMATOR_HORIZONTAL = "horizontal";
    private const string ANIMATOR_VERTICAL = "vertical";
    private const string ANIMATOR_ATTACKING = "attacking";

    private Enemy enemy;
    private Entity.Entity target;
    private Vector2 targetPosition;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Vector2 movement = targetPosition - (Vector2)enemy.transform.position;
        enemy.Animator.SetBool(ANIMATOR_ATTACKING, true);
    }

    public void Exit()
    {
        enemy.Animator.SetBool(ANIMATOR_ATTACKING, false);
    }

    public EnemyState Update(float deltaTime)
    {
        return null;
    }

    private Vector2 GetMovementToward(Vector2 targetPosition)
    {
        Vector2 currentPosition = enemy.transform.position;

        Vector2 direction = targetPosition - currentPosition;

        return direction.normalized;
    }

    public EnemyState FixedUpdate(float fixedDeltaTime)
    {
        Vector2 movementDirection = GetMovementToward(target.transform.position);
        enemy.Move(movementDirection);
        enemy.Animator.SetFloat(ANIMATOR_HORIZONTAL, movementDirection.x);
        enemy.Animator.SetFloat(ANIMATOR_VERTICAL, movementDirection.y);
        return null;
    }

    public EnemyState Input(Entity.Entity target)
    {
        if (target == null) return new EnemyPatrolState();
        return null;
    }
}
