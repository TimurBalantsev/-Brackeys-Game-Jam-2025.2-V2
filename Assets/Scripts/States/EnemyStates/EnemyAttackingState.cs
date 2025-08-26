using UnityEngine;

public class EnemyAttackingState : EnemyState
{
    private const string ANIMATOR_ATTACK_TRIGGER = "attack";

    private Enemy enemy;
    private AnimatorStateInfo animatorStateInfo;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;

        enemy.Animator.SetTrigger(ANIMATOR_ATTACK_TRIGGER);
    }

    public void Exit()
    {
    }

    public EnemyState Update(float deltaTime)
    {
        animatorStateInfo = enemy.Animator.GetCurrentAnimatorStateInfo(0);

        // Middle of animation
        if (animatorStateInfo.normalizedTime >= 0.5f)
        {
            enemy.Attack();

            return new EnemyIdleState();
        }

        return null;
    }

    public EnemyState FixedUpdate(float fixedDeltaTime)
    {
        return null;
    }

    public EnemyState Input(Entity.Entity target)
    {
        if (target == null) return new EnemyIdleState();
        return null;
    }
}