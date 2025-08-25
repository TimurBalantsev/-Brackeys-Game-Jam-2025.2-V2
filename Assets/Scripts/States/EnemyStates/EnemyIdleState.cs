using UnityEngine;

class EnemyIdleState : EnemyState
{
    private Enemy enemy;
    private float idleTimer;
    private float maxIdleTime;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        idleTimer = 0f;

        maxIdleTime = Random.Range(0f, enemy.maxIdleTime);
    }

    public void Exit()
    {

    }

    public EnemyState Update(float deltaTime)
    {
        idleTimer += deltaTime;

        if (idleTimer >= maxIdleTime)
        {
            return new EnemyPatrolState();
        }

        return null;
    }

    public EnemyState Input(Entity.Entity target)
    {
        if (target != null) return new EnemyChasingState();
        return null;
    }
}
