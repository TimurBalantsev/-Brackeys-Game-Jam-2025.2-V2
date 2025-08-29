
using UnityEngine;

public class PlayerWalkingState : PlayerState
{
    private const string ANIMATOR_HORIZONTAL = "horizontal";
    private const string ANIMATOR_VERTICAL = "vertical";
    private const string ANIMATOR_WALKING = "walking";

    private Player player;
    private Vector2 lastDirection = Vector2.zero;

    public void Enter(Player player)
    {
        this.player = player;
        this.player.walkSoundLoop.Play();

        player.Animator.SetBool(ANIMATOR_WALKING, true);
    }
    public void Exit()
    {
        this.player.walkSoundLoop.Stop();

        player.Animator.SetBool(ANIMATOR_WALKING, false);
    }

    public PlayerState Update(float deltaTime)
    {
        return null;
        // Vector2 movementDirection = InputManager.Instance.GetMovementDirection();
        // HandleAnimation(movementDirection);
        // player.Move(movementDirection);
        // return null;
    }

    public PlayerState FixedUpdate(float fixedDeltaTime)
    {
        Vector2 movementDirection = InputManager.Instance.GetMovementDirection();
        player.Move(movementDirection);
        player.Animator.SetFloat(ANIMATOR_HORIZONTAL, movementDirection.x);
        player.Animator.SetFloat(ANIMATOR_VERTICAL, movementDirection.y);
        player.lastMovement = movementDirection;
        return null;
    }

    public PlayerState Input(InputManager.InputActions inputAction)
    {
        switch (inputAction)
        {
            case InputManager.InputActions.MOVE:
                return null;
            case InputManager.InputActions.NONE:
                return new PlayerIdleState();
            // case InputManager.InputActions.ATTACK:
            //     return new PlayerAttackingState();
            default:
                return null;
        }
    }
}
