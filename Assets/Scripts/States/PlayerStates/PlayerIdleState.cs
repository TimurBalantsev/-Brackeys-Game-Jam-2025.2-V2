using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private const string ANIMATOR_HORIZONTAL = "horizontal";
    private const string ANIMATOR_VERTICAL = "vertical";

    private Player player;

    public void Enter(Player player)
    {
        this.player = player;

        player.Animator.SetFloat(ANIMATOR_HORIZONTAL, player.lastMovement.x);
        player.Animator.SetFloat(ANIMATOR_VERTICAL, player.lastMovement.y);
    }

    public void Exit()
    {
    }

    public PlayerState Update(float deltaTime)
    {
        //do nothing
        return null;
    }

    public PlayerState Input(InputManager.InputActions inputAction)
    {
        if (!player.canMove)
        {
            return null;
        }
        switch (inputAction)
        {
            case InputManager.InputActions.MOVE:
                return new PlayerWalkingState();
            case InputManager.InputActions.NONE:
                return null;
            // case InputManager.InputActions.ATTACK:
            //     return new PlayerAttackingState();
            default:
                return null;
        }
    }
}
