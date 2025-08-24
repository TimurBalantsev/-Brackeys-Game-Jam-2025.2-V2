
public interface PlayerState
{
    public void Enter(Player player);
    void Exit();
    
    PlayerState Update(float deltaTime);
    PlayerState Input(InputManager.InputActions inputAction);

    PlayerState FixedUpdate(float fixedDeltaTime)
    {
        return null;
    }
}
