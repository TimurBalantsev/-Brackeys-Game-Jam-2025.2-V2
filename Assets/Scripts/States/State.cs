using UnityEngine;

public interface State
{
    void Enter();
    void Exit();
    
    State Update(float deltaTime);
    State Input();
}
