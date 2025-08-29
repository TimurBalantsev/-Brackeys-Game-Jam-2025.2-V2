
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    private const string ANIMATOR_HORIZONTAL = "horizontal";
    private const string ANIMATOR_VERTICAL = "vertical";
    private const string ANIMATOR_DIE = "die";

    private Player player;

    public void Enter(Player player)
    {
        this.player = player;
        
        this.player.Animator.SetFloat(ANIMATOR_HORIZONTAL, player.lastMovement.x);
        this.player.Animator.SetFloat(ANIMATOR_VERTICAL, player.lastMovement.y);
        this.player.Animator.SetTrigger(ANIMATOR_DIE);
    }

    public void Exit()
    {
    }

    public PlayerState Input(InputManager.InputActions inputAction)
    {
        return null;
    }

    public PlayerState Update(float deltaTime)
    {
        return null;
    }
}
