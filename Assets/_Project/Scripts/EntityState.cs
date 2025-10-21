using UnityEngine;

public abstract class EntityState
{
    public enum GameState
    {
        Idle,
        Move,
        Jump,
        Attack,
        Dead
    }

    protected Player _player;
    protected StateMachine _stateMachine;
    protected GameState _stateName;

    public EntityState(Player player, StateMachine stateMachine, GameState stateName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _stateName = stateName;
    }

    public virtual void Enter()
    {
        Debug.Log($"I entered {_stateName}.");
    }

    public virtual void Update()
    {
        Debug.Log($"I am updating {_stateName}.");
    }

    public virtual void Exit()
    {
        Debug.Log($"I am exiting {_stateName}.");
    }
}
