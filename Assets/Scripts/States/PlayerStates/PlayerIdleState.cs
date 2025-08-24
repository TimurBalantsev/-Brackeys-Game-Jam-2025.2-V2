using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private Player player;

    public void Enter(Player player)
    {
        this.player = player;
        player.Animator.Play("player_idle");
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
