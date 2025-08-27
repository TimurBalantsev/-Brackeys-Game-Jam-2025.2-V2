
using UnityEngine;

public class PlayerWalkingState : PlayerState
{
    private Player player;
    private Vector2 lastDirection = Vector2.zero;

    public void Enter(Player player)
    {
        this.player = player;
        HandleAnimation(InputManager.Instance.GetMovementDirection());
        this.player.walkSoundLoop.Play();
    }
    
    private void HandleAnimation(Vector2 direction)
    {
        if (direction == lastDirection) return;
        if (direction.y < 0)
        {
            lastDirection = direction;
            player.Animator.Play("walk_down");
        } else if (direction == Vector2.up)
        {
            lastDirection = direction;
            player.Animator.Play("walk_up");
        }
        else if (direction == Vector2.left)
        {
            lastDirection = direction;
            player.Animator.Play("walk_left");
        }
        else if (direction == Vector2.right)
        {
            lastDirection = direction;
            player.Animator.Play("walk_right");
        } else if (direction == (Vector2.up + Vector2.right).normalized)
        {
            lastDirection = direction;
            player.Animator.Play("walk_up_right");
        } else if (direction == (Vector2.up + Vector2.left).normalized)
        {
            lastDirection = direction;
            player.Animator.Play("walk_up_left");
        }
    }

    public void Exit()
    {
        this.player.walkSoundLoop.Stop();
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
        HandleAnimation(movementDirection);
        player.Move(movementDirection);
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
