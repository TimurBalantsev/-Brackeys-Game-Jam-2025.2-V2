using System;
using UnityEngine;

public class Enemy : Entity.Entity
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyFOV enemyFOV;

    private EnemyState activeState;
    public Animator Animator => animator;

    public Entity.Entity Target { get; private set; }

    private Vector2 lastMovementDirection;

    private void OnEnable()
    {
        enemyFOV.OnTargetSpotted += EnemyFOV_OnTargetSpotted;
        enemyFOV.OnTargetLost += EnemyFOV_OnTargetLost;
    }

    private void OnDisable()
    {
        enemyFOV.OnTargetSpotted -= EnemyFOV_OnTargetSpotted;
        enemyFOV.OnTargetLost -= EnemyFOV_OnTargetLost;
    }

    private void EnemyFOV_OnTargetSpotted(Entity.Entity target)
    {
        Target = target;
        EnemyState newState = activeState.Input(target);
        if (newState != null) ChangeState(newState);
    }

    private void EnemyFOV_OnTargetLost()
    {
        Target = null;
        EnemyState newState = activeState.Input(null);
        if (newState != null) ChangeState(newState);
    }

    private void Start()
    {
        ChangeState(new EnemyPatrolState());
    }

    private void Update()
    {
        StateUpdate();

        UpdateFOVRotation(lastMovementDirection);
    }

    private void FixedUpdate()
    {
        StateFixedUpdate();
    }

    private void StateFixedUpdate()
    {
        EnemyState newState = activeState.FixedUpdate(Time.deltaTime);
        if (newState != null) ChangeState(newState);
    }

    private void ChangeState(EnemyState newState)
    {
        activeState?.Exit();
        activeState = newState;
        activeState.Enter(this);
    }

    private void StateUpdate()
    {
        EnemyState newState = activeState.Update(Time.deltaTime);
        if (newState != null) ChangeState(newState);
    }

    public void Move(Vector2 movementDirection)
    {
        Vector2 newPosition = rigidBody.position + movementDirection * (stats.speed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPosition);
        lastMovementDirection = movementDirection;
    }

    private void UpdateFOVRotation(Vector2 movementDirection)
    {
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

        enemyFOV.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    protected override void Die()
    {
        Debug.Log($"Enemy {name} died");
    }
}
