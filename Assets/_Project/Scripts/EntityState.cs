using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    public enum EnumState
    {
        Idle,
        Move,
    }

    protected Player _player;
    protected Animator _animator;
    protected Rigidbody2D _rb;
    protected StateMachine _stateMachine;
    protected EnumState _stateName;

    protected Dictionary<EnumState, string> _enumToAnimStringDictionary = new()
    {
        {EnumState.Idle, "idle" },
        {EnumState.Move, "move" }
    };

    public EntityState(Player player, StateMachine stateMachine, EnumState stateName)
    {
        _player = player;
        _animator = _player.PlayerAnimator;
        _rb = _player.Rb;
        _stateMachine = stateMachine;
        _stateName = stateName;
    }

    public virtual void Enter()
    {
        _animator.SetBool(_enumToAnimStringDictionary[_stateName], true);
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
        _animator.SetBool(_enumToAnimStringDictionary[_stateName], false);
    }
}
