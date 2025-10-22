using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class EntityState
{
    public enum EnumState
    {
        Idle,
        Move,
        JumpFall,
        WallSlide,
        Dash
    }

    protected Animator _animator;
    protected Rigidbody2D _rb;
    protected Player _player;
    protected PlayerInputActions _inputs;
    protected StateMachine _stateMachine;
    protected EnumState _stateName;
    protected const string Y_VELOCITY_ANIM_NAME = "yVelocity";
    protected float _stateTimer;

    protected Dictionary<EnumState, string> _enumToAnimStringDictionary = new()
    {
        {EnumState.Idle, "idle" },
        {EnumState.Move, "move" },
        {EnumState.JumpFall, "jumpFall" },
        {EnumState.WallSlide, "wallSlide" },
        {EnumState.Dash, "dash" }
    };

    public EntityState(Player player, StateMachine stateMachine, EnumState stateName)
    {
        _player = player;
        _inputs = _player.Inputs;
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
        _stateTimer -= Time.deltaTime;
        _animator.SetFloat(Y_VELOCITY_ANIM_NAME, _rb.linearVelocityY);

        if (_inputs.Player.Dash.WasPressedThisFrame() && CanDash())
            _stateMachine.ChangeState(_player.DashState);
    }

    public virtual void Exit()
    {
        _animator.SetBool(_enumToAnimStringDictionary[_stateName], false);
    }

    private bool CanDash()
    {
        return !_player.WallDetected && _stateMachine.CurrentState != _player.DashState;
    }
}
