
public interface EnemyState
{
    public void Enter(Enemy enemy);
    void Exit();

    EnemyState Update(float deltaTime);

    EnemyState Input(Entity.Entity target);
    EnemyState FixedUpdate(float fixedDeltaTime)
    {
        return null;
    }
}
