using UnityEngine;

class EnemyPatrolState : EnemyState
{
    private const string ANIMATOR_HORIZONTAL = "horizontal";
    private const string ANIMATOR_VERTICAL = "vertical";
    private const string ANIMATOR_WALKING = "walking";

    private Enemy enemy;
    private Vector2 targetPosition;

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
        return null;
    }

    private Vector2 GetNextTargetPosition()
    {
        Vector3Int currentCell = enemy.Tilemap.WorldToCell(enemy.transform.position);
        Vector3Int randomCell;
        int iterations = 0;

        do
        {
            int x = currentCell.x + Random.Range(-enemy.maxPatrolTileDistance, enemy.maxPatrolTileDistance + 1);
            int y = currentCell.y + Random.Range(-enemy.maxPatrolTileDistance, enemy.maxPatrolTileDistance + 1);
            randomCell = new Vector3Int(x, y, 0);

            iterations++;
            if (iterations > 1000)
            {
                Debug.LogWarning("Could not find a nearby free tile!");
                return enemy.transform.position;
            }

        }
        while (!enemy.IsTileFree(randomCell));

        return (Vector2)enemy.Tilemap.GetCellCenterWorld(randomCell);
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

        if (distance < 0.1f)
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
        if (target != null) return new EnemyChaseState();
        return null;
    }
}
