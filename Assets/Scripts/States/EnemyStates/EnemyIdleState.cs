using UnityEngine;

class EnemyIdleState : EnemyState
{
    private const string ANIMATOR_HORIZONTAL = "horizontal";
    private const string ANIMATOR_VERTICAL = "vertical";
    private const string ANIMATOR_WALKING = "walking";

    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;

        enemy.Animator.SetBool(ANIMATOR_WALKING, false);
    }

    public void Exit()
    {

    }

    public EnemyState Update(float deltaTime)
    {
        return null;
    }

    public EnemyState Input(Entity.Entity target)
    {
        if (target != null) return new EnemyChaseState();
        return null;
    }
}
