
using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    //todo add speed modifiers etc... and play animation yourself with the speed of the attack. 
    private Player player;
    private AnimatorStateInfo animatorStateInfo;
    private InputManager.InputActions lastInput;
    private bool attackFinished = false;

    public void Enter(Player player)
    {
        this.player = player;
        player.Animator.Play("orc_attack_01", 0, 0f);
    }

    public void Exit()
    {
        
    }

    public PlayerState Update(float deltaTime)
    {
        animatorStateInfo = player.Animator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.normalizedTime >= 1f)
        {
            attackFinished = true;
        }
        
        Vector2 movementDirection = InputManager.Instance.GetMovementDirection();
        player.Move(movementDirection);

        return null;
    }

    public PlayerState Input(InputManager.InputActions inputAction)
    {
        if (!attackFinished) return null;
        switch (inputAction)
        {
            case InputManager.InputActions.MOVE:
                return new PlayerWalkingState();
            case InputManager.InputActions.NONE:
                return new PlayerIdleState();
            case InputManager.InputActions.ATTACK:
                return new PlayerAttackingState();
            default:
                return null;
        }
    }
}
