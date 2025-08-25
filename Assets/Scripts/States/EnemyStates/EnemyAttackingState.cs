using UnityEngine;

public class EnemyAttackingState : EnemyState
{
    private const string ANIMATOR_ATTACK_TRIGGER = "attack";

    private Enemy enemy;
    private AnimatorStateInfo animatorStateInfo;
    private bool attackFinished = false;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        attackFinished = false;

        enemy.Animator.SetTrigger(ANIMATOR_ATTACK_TRIGGER);
    }

    public void Exit()
    {
    }

    public EnemyState Update(float deltaTime)
    {
        animatorStateInfo = enemy.Animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.normalizedTime >= 1f)
        {

            attackFinished = true;
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