using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : EntityState
{
    public PlayerDashState(Player player, StateMachine stateMachine, EnumState stateName) : base(player, stateMachine, stateName)
    {
    }

    #region Event Fields
    #endregion

    #region Public Fields
    #endregion

    #region Serialized Private Fields
    #endregion

    #region Private Fields
    private float _originalGravityScale;
    private int _dashDirection;
    #endregion

    #region Public Properties
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public override void Enter()
    {
        base.Enter();
        _stateTimer = _player.DashDuration;

        _originalGravityScale = _rb.gravityScale;
        _rb.gravityScale = 0f;
        _dashDirection = _player.FacingDirection;
    }

    public override void Update()
    {
        base.Update();

        CancelDashChecks();

        _player.SetVelocity(_player.DashSpeed * _dashDirection, 0f);

        if (_stateTimer < 0f)
        {
            if (_player.GroundDetected)
                _stateMachine.ChangeState(_player.IdleState);
            else
                _stateMachine.ChangeState(_player.FallState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetVelocity(0f, 0f);
        _rb.gravityScale = _originalGravityScale;
    }
    #endregion

    #region Private Methods
    private void CancelDashChecks()
    {
        if (_player.WallDetected)
        {
            if (_player.GroundDetected)
                _stateMachine.ChangeState(_player.IdleState);
            else
                _stateMachine.ChangeState(_player.WallSlideState);
        }
    }
    #endregion
}